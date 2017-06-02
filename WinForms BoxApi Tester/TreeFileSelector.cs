using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Extensions;

//https://www.codeproject.com/Articles/13099/Loading-and-Saving-a-TreeView-control-to-an-XML-fi

namespace FileFolderSelector
{
    public class TreeNodeCustomData
    {
        public string BasePath { get; internal set; }
        public string FileFilter { get; set; }
        public string FullFilePath { get; set; }
        public bool IsDirectory { get; internal set; }
    }

    public class NodeChangedEventArgs : EventArgs
    {
        public System.Windows.Forms.TreeNode SelectedNode;
        public NodeChangedEventArgs(System.Windows.Forms.TreeNode selectedNode)
        {
            SelectedNode = selectedNode;
        }
    }

    public partial class TreeFileSelector : TreeView
    {
        public event EventHandler<NodeChangedEventArgs> PropertyChanged;

        private System.Windows.Forms.TreeNode _currentSelectedNode;
        public TreeNode CurrentSelectedNode
        {
            get
            {
                return _currentSelectedNode;
            }
            set
            {
               if(_currentSelectedNode != value)
                    {
                        _currentSelectedNode = value;
                        OnNodeChanged(new NodeChangedEventArgs(_currentSelectedNode));
                    } 
                
            }
        }

        #region Hide / Show Files on Node
        internal void DisplayFilesInSelectedNode()
        {
            var path = CurrentSelectedNode.FullPath;
            DirectoryInfo info = new DirectoryInfo(path);
            foreach( FileInfo fileInfo in info.EnumerateFiles())
            {
                CurrentSelectedNode.Nodes.Add(fileInfo.Name);
            }
        }

        internal void RemoveFilesFromSelectedNode()
        {
            List<TreeNode> targetedForRemoval = new List<TreeNode>();
            foreach(TreeNode node in CurrentSelectedNode.Nodes)
            {
                if(node.Tag==null)
                {
                    targetedForRemoval.Add(node);
                }
            }
            foreach(TreeNode node in targetedForRemoval)
            {
                this.Nodes.Remove(node);
            }
            

            //CurrentSelectedNode.Nodes.Clear();
            
        }
        #endregion

        protected virtual void OnNodeChanged(NodeChangedEventArgs e)
        {
            PropertyChanged.Invoke(this,e);
        }

        public TreeFileSelector()
        {
            InitializeComponent();
            this.CheckBoxes = true;          
            this.AfterCheck += TreeFileSelector_AfterCheck;
            this.AfterSelect += TreeFileSelector_AfterSelect;  
        }

        private void TreeFileSelector_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CurrentSelectedNode = e.Node;
        }
        
        private void TreeFileSelector_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //select / deselect all files in folder if folder selected
            TreeNode currentNode = e.Node;

            var tag = currentNode.Tag;
            //can't check files
            if(tag == null && currentNode.Checked)
            {
                currentNode.Checked = false;
                return;
            }
            //check all parents 
            if(currentNode.Checked)
            {
                RecurseUpParents(currentNode);
                return;
            }
            if(!currentNode.Checked && UncheckedNodeHasCheckedChildFolders(currentNode))
            {
                currentNode.Checked = true;
                return;
            }
        }

        private bool UncheckedNodeHasCheckedChildFolders(TreeNode currentNode)
        {
            List<TreeNode> flatNodes = new List<TreeNode>();
            flatNodes.Add(currentNode);
            CollectChildNodes(flatNodes,currentNode);
            return flatNodes.Any(n => n.Checked);
        }

        private void CollectChildNodes(List<TreeNode> flatNodes, TreeNode currentNode)
        {
            foreach(TreeNode node in currentNode.Nodes)
            {
                flatNodes.Add(node);
                CollectChildNodes(flatNodes, node);
            }
        }

        private void RecurseUpParents(System.Windows.Forms.TreeNode node)
        {
            if (node.Parent != null)
            {
                if(node.Parent.Checked == false)
                {
                    node.Parent.Checked = true;
                }
                else
                {
                    RecurseUpParents(node.Parent);
                }
            }
        }

        private TreeNodeCustomData CreateDirectoryTag(TreeNode node)
        {
            TreeNodeCustomData tag = new TreeNodeCustomData();
            tag.IsDirectory = true;
            tag.BasePath = node.TopAncestor().FullPath; 
            return tag;
        }

        internal void BuildTreeFromRootDirectory(DirectoryInfo directoryInfo, TreeNodeCollection topNodes, bool foldersOnly)
        {
            var bp =  System.IO.Directory.GetParent(directoryInfo.FullName);
            if(bp!=null)
            {
                //BasePath = bp.FullName;
                var rootFolderStructure = topNodes.Add(bp.FullName);
                rootFolderStructure.Tag = CreateDirectoryTag(rootFolderStructure);

                var firstDataNode = rootFolderStructure.Nodes.Add(directoryInfo.Name);
                firstDataNode.Tag = CreateDirectoryTag(firstDataNode);

                BuildTree(directoryInfo, firstDataNode);
            }        
        }
        private void BuildTree(DirectoryInfo directoryInfo, System.Windows.Forms.TreeNode currentNode)
        {
            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                var childNode = currentNode.Nodes.Add(subdir.Name);
                childNode.Tag = CreateDirectoryTag(childNode);

                BuildTree(subdir, childNode);
            }
        }



        #region Save File Section to XML
        internal void SaveCurrentSelection(string fileName)
        {
            if(File.Exists(fileName))
            {
                XmlTextWriter textWriter = new XmlTextWriter(fileName, System.Text.Encoding.ASCII);
                // writing the xml declaration tag
                textWriter.WriteStartDocument();
                //textWriter.WriteRaw("\r\n");
                // writing the main tag that encloses all node tags
                textWriter.WriteStartElement("TreeView");

                // save the nodes, recursive method
                SaveNodes(this.Nodes, textWriter);

                textWriter.WriteEndElement();

                textWriter.Close();
            }


        }

        // Xml tag for node, e.g. 'node' in case of <node></node>
        private const string XmlNodeTag = "node";
        // Xml attributes for node e.g. <node text="Asia" tag="" 
        // imageindex="1"></node>
        private const string XmlNodeTextAtt = "text";
        private const string XmlNodeCheckedAtt = "checked";
        private const string XmlNodeExpandedAtt = "expanded";
        private const string XmlNodeTagAtt = "tag";
        private const string XmlNodeImageIndexAtt = "imageindex";

        private void SaveNodes(TreeNodeCollection nodesCollection,XmlTextWriter textWriter)
        {
            for (int i = 0; i < nodesCollection.Count; i++)
            {
                System.Windows.Forms.TreeNode node = nodesCollection[i];
                textWriter.WriteStartElement(XmlNodeTag);
                textWriter.WriteAttributeString(XmlNodeTextAtt,
                                                           node.Text);
                //add checked
                textWriter.WriteAttributeString(XmlNodeCheckedAtt,
                                                           node.Checked.ToString());

                //add expanded
                textWriter.WriteAttributeString(XmlNodeExpandedAtt,
                                                           node.IsExpanded.ToString());

                textWriter.WriteAttributeString(
                    XmlNodeImageIndexAtt, node.ImageIndex.ToString());
                if (node.Tag != null)
                    textWriter.WriteAttributeString(XmlNodeTagAtt,
                                                node.Tag.ToString());
                // add other node properties to serialize here  
                if (node.Nodes.Count > 0)
                {
                    SaveNodes(node.Nodes, textWriter);
                }
                textWriter.WriteEndElement();
            }
        }


        #endregion Save File Selection to XML

        #region Load Selection From File
        internal void LoadSavedSelection(string fileName)
        {
            if(File.Exists(fileName))
            {
                DeserializeTreeView(this, fileName);
            }
        }
        private void DeserializeTreeView(TreeView treeView, string fileName)
        {
            XmlTextReader reader = null;
            try
            {
                // disabling re-drawing of treeview till all nodes are added
                treeView.BeginUpdate();
                reader = new XmlTextReader(fileName);
                System.Windows.Forms.TreeNode parentNode = null;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            System.Windows.Forms.TreeNode newNode = new System.Windows.Forms.TreeNode();
                            bool isEmptyElement = reader.IsEmptyElement;

                            // loading node attributes
                            int attributeCount = reader.AttributeCount;
                            if (attributeCount > 0)
                            {
                                for (int i = 0; i < attributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    SetAttributeValue(newNode,
                                                 reader.Name, reader.Value);
                                }
                            }
                            // add new node to Parent Node or TreeView
                            if (parentNode != null)
                                parentNode.Nodes.Add(newNode);
                            else
                                treeView.Nodes.Add(newNode);

                            // making current node 'ParentNode' if its not empty
                            if (!isEmptyElement)
                            {
                                parentNode = newNode;
                            }
                        }
                    }
                    // moving up to in TreeView if end tag is encountered
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            parentNode = parentNode.Parent;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.XmlDeclaration)
                    {
                        //Ignore Xml Declaration                    
                    }
                    else if (reader.NodeType == XmlNodeType.None)
                    {
                        return;
                    }
                    else if (reader.NodeType == XmlNodeType.Text)
                    {
                        parentNode.Nodes.Add(reader.Value);
                    }

                }
            }
            finally
            {
                // enabling redrawing of treeview after all nodes are added
                treeView.EndUpdate();
                reader.Close();
                treeView.ExpandAll();
            }
        }
        /// <span class="code-SummaryComment"><summary></span>
        /// Used by Deserialize method for setting properties of
        /// TreeNode from xml node attributes
        /// <span class="code-SummaryComment"></summary></span>
        private void SetAttributeValue(System.Windows.Forms.TreeNode node,
                           string propertyName, string value)
        {
            if (propertyName == XmlNodeTextAtt)
            {
                node.Text = value;
            }
            else if (propertyName == XmlNodeImageIndexAtt)
            {
                node.ImageIndex = int.Parse(value);
            }
            else if (propertyName == XmlNodeTagAtt)
            {
                node.Tag = value;
            }
            else if (propertyName == XmlNodeCheckedAtt)
            {
                bool isChecked = false;
                bool.TryParse( value, out isChecked);
                node.Checked = isChecked;
            }
            else if (propertyName == XmlNodeExpandedAtt)
            {
                bool isExpanded = false;
                bool.TryParse(value, out isExpanded);
                if(isExpanded)
                {
                    node.Expand();
                }
                
            }
        }

        #endregion Load Selection From File

        #region Clear All Nodes
        internal void ClearAllNodes()
        {
            this.Nodes.Clear();
        }
        #endregion Clear All Nodes
    }
}
