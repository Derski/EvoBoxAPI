using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;
using EvoBoxAPI;
using System.Deployment.Application;

namespace FileFolderSelector
{
    public partial class FileFolderSelectForm : Form
    {
        private DirectoryInfo directoryInfo;
        private string _lastSavedFileName = "";
        
        public TreeNodeCollection SelectedNodes { get; set; }
       // public string BasePath { get; set; }

        public FileFolderSelectForm(string lastSavedFileName)
        {
            InitializeComponent();
            _lastSavedFileName = lastSavedFileName;
            treeFileSelector.PropertyChanged += TreeFileSelector_PropertyChanged;
        }



        private void FileFolderSelectForm_Load(object sender, EventArgs e)
        {
            string baseFolderPath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"JobWork");
            SelectNewFolderStructure(baseFolderPath);
        }

        #region Node Custom Properties
        private void TreeFileSelector_PropertyChanged(object sender, NodeChangedEventArgs e)
        {
            var selectedNode = e.SelectedNode;
            label_NodeFullPath.Text = selectedNode.FullPath;

        }
        private void button_SetNodeFilter_Click(object sender, EventArgs e)
        {
            if(treeFileSelector.SelectedNode == null ||
                treeFileSelector.SelectedNode.Tag == null)
            {
                MessageBox.Show("Please Select a valid Directory Node to set filter");
            }
            else
            {
                if(treeFileSelector.SelectedNode.Tag is TreeNodeCustomData)
                {
                    TreeNodeCustomData tag = (TreeNodeCustomData)treeFileSelector.SelectedNode.Tag;
                    var filter = textBox_SelectedNodeFilter.Text;
                    tag.FileFilter = filter;
                    label_FolderFilter.Text = filter;
                }
                
            }
        }
        #endregion Node Custom Properties

        #region Clear
        private void button_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            treeFileSelector.ClearAllNodes();
        }
        #endregion Clear

        #region Save
        private void button_Save_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ApplicationDeployment.IsNetworkDeployed.ToString());
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var networkDeployedFile = TryGetDeploymentFileName();
                try
                {
                    treeFileSelector.SaveCurrentSelection(networkDeployedFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw ex;
                }
                
            }
            else
            {
                treeFileSelector.SaveCurrentSelection(_lastSavedFileName);
            }
            
        }
        #endregion

        private string TryGetDeploymentFileName()
        {
            string fileName = "";
            try
            {
                fileName = ApplicationDeployment.CurrentDeployment.DataDirectory
                    + @"\LastSavedLocalFolderStructure.xml";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read file. Error message: " + ex.Message);
            }
            return fileName;
        }

        #region Load
        private void button_Load_Click(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                _lastSavedFileName = TryGetDeploymentFileName();
            }
            treeFileSelector.LoadSavedSelection(_lastSavedFileName);
        }
        #endregion Load

        #region Select New Folder
        private void button_Select_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog SelectFolderDialogue = new FolderBrowserDialog();
            SelectFolderDialogue.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var result = SelectFolderDialogue.ShowDialog();
            if(result.Equals(DialogResult.OK))
            {
                SelectNewFolderStructure(SelectFolderDialogue.SelectedPath);
            }
        }

        private void SelectNewFolderStructure(string baseFolderPath)
        {
            directoryInfo = new DirectoryInfo(baseFolderPath);
            if (directoryInfo.Parent != null)
            {
                treeFileSelector.BuildTreeFromRootDirectory
                    (directoryInfo, treeFileSelector.Nodes, checkBox_FoldersOnly.Checked);
            }
            else
            {
                MessageBox.Show("You can't select a drive root folder");
            }
        }
        #endregion

        #region Close Form
        private void button_OK_Click(object sender, EventArgs e)
        {
            OK();
        }
        private void OK()
        {
            this.SelectedNodes = treeFileSelector.Nodes;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        private void Cancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion Close Form

        #region Show / hide Files on Selected Folder Node
        private void button_ShowNodeFiles_Click(object sender, EventArgs e)
        {
            ShowFilesOnSelectedFolderNode();
        }
        private void ShowFilesOnSelectedFolderNode()
        {
            treeFileSelector.DisplayFilesInSelectedNode();
        }
        private void button_HideNodeFiles_Click(object sender, EventArgs e)
        {
            HideFilesOnSelectedFolder();
        }

        private void HideFilesOnSelectedFolder()
        {
            treeFileSelector.RemoveFilesFromSelectedNode();
        }

        #endregion


    }
}
