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
using EvoBoxAPILibrary;

//https://www.codeproject.com/Articles/13099/Loading-and-Saving-a-TreeView-control-to-an-XML-fi

namespace FileFolderSelector
{

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
        TreeNodeXMLSerializer _xMLSerializer;
        TreeNodeXMLSerializer XMLSerializer
        {
            get
            {
                if(_xMLSerializer == null)
                {
                    _xMLSerializer = new TreeNodeXMLSerializer();
                }
                return _xMLSerializer;
            }
        }
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
            if (CurrentSelectedNode != null && !string.IsNullOrEmpty(CurrentSelectedNode.FullPath))
            {
                var path = CurrentSelectedNode.FullPath;

                //Don't do anything if a file
                if (Path.HasExtension(path)) return;

                DirectoryInfo info = new DirectoryInfo(path);
                foreach (FileInfo fileInfo in info.EnumerateFiles())
                {
                    CurrentSelectedNode.Nodes.Add(fileInfo.Name);
                }
            }
            else
            {
                
            }
        }

        internal void RemoveFilesFromSelectedNode()
        {
            if(CurrentSelectedNode!= null)
            {
                List<TreeNode> targetedForRemoval = new List<TreeNode>();
                foreach (TreeNode node in CurrentSelectedNode.Nodes)
                {
                    if (node.Tag == null)
                    {
                        targetedForRemoval.Add(node);
                    }
                }
                foreach (TreeNode node in targetedForRemoval)
                {
                    this.Nodes.Remove(node);
                }
            }   
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
            tag.FileFilter = "*.*";//default file filter
            tag.FullFilePath = node.FullPath;
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
        internal void SaveCurrentSelection(string fileName,string clientId, string jobId)
        {
            XMLSerializer.SaveCurrentSelection(fileName, this.Nodes, clientId,jobId);
        }
        #endregion Save File Selection to XML

        #region Load Selection From File
        internal void LoadSavedSelection(string fileName, out string clientJobIdInfo)
        {
            clientJobIdInfo = "";
            XMLSerializer.LoadSavedSelection(fileName, this,out clientJobIdInfo);

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
