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
using EvoBoxAPILibrary;
using System.Deployment.Application;
using System.Text.RegularExpressions;

namespace FileFolderSelector
{
    public partial class FileFolderSelectForm : Form
    {
        private DirectoryInfo directoryInfo;
        
        private string _lastSavedFileName
        {
            get
            {
                var _file = Path.Combine
                    (Environment.CurrentDirectory, "LastSavedLocalFolderStructure.xml");
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    _file = TryGetDeploymentFileName(); 

                }
                return _file;
            }
        }

        public TreeNodeCollection SelectedNodes { get; set; }
       // public string BasePath { get; set; }



        public FileFolderSelectForm()
        {
            InitializeComponent();
            treeFileSelector.PropertyChanged += TreeFileSelector_PropertyChanged;
        }



        private void FileFolderSelectForm_Load(object sender, EventArgs e)
        {
            LoadTreeFromXML();
        }

        #region Node Custom Properties
        private void TreeFileSelector_PropertyChanged(object sender, NodeChangedEventArgs e)
        {
            var selectedNode = e.SelectedNode;
            label_NodeFullPath.Text = selectedNode.FullPath;
            if(selectedNode.Tag != null)
            {
                TreeNodeCustomData customTag = (TreeNodeCustomData)selectedNode.Tag;
                textBox_SelectedNodeFilter.Text = customTag.FileFilter;
            }
        }
        private void button_SetNodeFilter_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(errorProvider1.GetError(textBox_SelectedNodeFilter)))
            {
                if (treeFileSelector.SelectedNode == null ||
                    treeFileSelector.SelectedNode.Tag == null)
                {
                    ShowSelectValidDirectoryMessage();
                }
                else
                {
                    if (treeFileSelector.SelectedNode.Tag is TreeNodeCustomData)
                    {
                        TreeNodeCustomData tag = (TreeNodeCustomData)treeFileSelector.SelectedNode.Tag;
                        var filter = textBox_SelectedNodeFilter.Text;
                        tag.FileFilter = filter;
                        label_FolderFilter.Text = filter;
                    }
                }
            }
        }
        #endregion Node Custom Properties

        private void ShowSelectValidDirectoryMessage()
        {
            MessageBox.Show("Please Select a valid Directory Node to set filter");
        }


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
            treeFileSelector.SaveCurrentSelection(_lastSavedFileName);
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
            LoadTreeFromXML();
        }
        private void LoadTreeFromXML()
        {
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
                    (directoryInfo, treeFileSelector.Nodes, true);
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
            if (treeFileSelector.SelectedNode == null)
            {
                ShowSelectValidDirectoryMessage();
            }
            else
            {
                treeFileSelector.RemoveFilesFromSelectedNode(); 
                treeFileSelector.DisplayFilesInSelectedNode();
            }
           
        }
  
        private void button_HideNodeFiles_Click(object sender, EventArgs e)
        {
            if (treeFileSelector.SelectedNode == null)
            {
                ShowSelectValidDirectoryMessage();
            }
            else
            {
                treeFileSelector.RemoveFilesFromSelectedNode();
            }
        }
        
        #endregion

        private void textBox_SelectedNodeFilter_Validating(object sender, CancelEventArgs e)
        {
            var filter = @"(\*.\w+$(\|\*.\w+)*)|\*.\*";
            Regex rgx = new Regex(filter, RegexOptions.IgnoreCase);
            bool isMatch = rgx.IsMatch(textBox_SelectedNodeFilter.Text);
            if(isMatch)
            {
                errorProvider1.SetError(textBox_SelectedNodeFilter, "");
            }
            else
            {
                errorProvider1.SetError(textBox_SelectedNodeFilter, "Incorrect File Filter Expression\n"+
                    "Please specify the file filter in the form *.txt|*.evoset|*.csv");
            }
        }
    }
}
