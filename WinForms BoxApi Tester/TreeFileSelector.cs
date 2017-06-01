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

//https://www.codeproject.com/Articles/13099/Loading-and-Saving-a-TreeView-control-to-an-XML-fi

namespace FileFolderSelector
{

    public partial class TreeFileSelector : TreeView
    {
        public TreeFileSelector()
        {
            InitializeComponent();
            this.CheckBoxes = true;          
            this.AfterCheck += TreeFileSelector_AfterCheck;    
        }

        public string BasePath { get; set; }
        private void TreeFileSelector_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //select / deselect all files in folder if folder selected
            TreeNode currentNode = e.Node;
            
            if(currentNode.Checked)
            {
                RecurseUpParents(currentNode);
            }
        }

        private void RecurseUpParents(TreeNode node)
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

        private bool isDirectory(string nodePath)
        {
            bool isDirectory = false;
            string fullFolderPath = nodePath;
            if(BasePath != null)
            {
                fullFolderPath = Path.Combine(BasePath, nodePath);
            }
             
            FileAttributes attr = File.GetAttributes(fullFolderPath);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                isDirectory = true;
            }
            return isDirectory;
        }


        internal void BuildTreeFromRootDirectory(DirectoryInfo directoryInfo, TreeNodeCollection nodes, bool foldersOnly)
        {
            nodes.Clear();
            var bp =  System.IO.Directory.GetParent(directoryInfo.FullName);
            if(bp!=null)
            {
                BasePath = bp.FullName;
            }
            BuildTree(directoryInfo, nodes, foldersOnly);

        }
        private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe, bool foldersOnly)
        {  
            TreeNode curNode = (TreeNode)addInMe.Add(directoryInfo.Name);
            if(!foldersOnly)
            {
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    curNode.Nodes.Add(file.FullName, file.Name);
                }
            }
            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildTree(subdir, curNode.Nodes, foldersOnly);
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
                TreeNode node = nodesCollection[i];
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
        public void DeserializeTreeView(TreeView treeView, string fileName)
        {
            XmlTextReader reader = null;
            try
            {
                // disabling re-drawing of treeview till all nodes are added
                treeView.BeginUpdate();
                reader = new XmlTextReader(fileName);
                TreeNode parentNode = null;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            TreeNode newNode = new TreeNode();
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
        private void SetAttributeValue(TreeNode node,
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
