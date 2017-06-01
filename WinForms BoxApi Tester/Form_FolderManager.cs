using EvoBoxAPI;
using FileFolderSelector;
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


namespace WinForms_BoxApi_Tester
{
    public partial class Form1 : Form
    {
        FolderManager folderManager;
        private string _lastSavedLocalFolders;
        public List<EvoBoxFolder> EvoBoxFolders { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeDefaults();
        }

        private void InitializeDefaults()
        {
            UpdatePrefix();
            _lastSavedLocalFolders 
                = Path.Combine(Environment.CurrentDirectory, "LastSavedLocalFolderStructure.xml");
        }

        private void button_GetToken_Click(object sender, EventArgs e)
        {
            folderManager = new FolderManager();
            textBox_AdminToken.Text = folderManager.AdminToken;
            groupBox_FolderManager.Enabled = true;
        }

        #region Show Box Account Login Info
        private void button_BoxAccountInfo_Click(object sender, EventArgs e)
        {
            ShowBoxAccountInfo();
        }
        private void ShowBoxAccountInfo()
        {
            Form_BoxAccountInfo accountInfoForm = new Form_BoxAccountInfo();
            accountInfoForm.ShowDialog();
        }
        #endregion

        #region Client Id and Job Prefix
        private void textBox_ClientId_TextChanged(object sender, EventArgs e)
        {
            UpdatePrefix();
        }

        private void textBox_JobId_TextChanged(object sender, EventArgs e)
        {
            UpdatePrefix();
        }
        private void UpdatePrefix()
        {
            textBox_Prefix.Text = textBox_ClientId.Text + "_" + textBox_JobId.Text + "_";
        }
        #endregion Client Id and Job Prefix

        private void toolStripStatusLabel1_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(toolStripStatusLabel1.Text);
        }

        #region Select Local Folders
        private void button_LocalFolderSelection_Click(object sender, EventArgs e)
        {
            SelectLocalFolders();
        }
        private void SelectLocalFolders()
        {
            FileFolderSelectForm folderSelector = new FileFolderSelectForm(_lastSavedLocalFolders);
            var result =  folderSelector.ShowDialog();
            if(result == DialogResult.OK)
            {
                var basePath = folderSelector.BasePath;
                List<EvoBoxFolder> boxFolders = BoxFolderStructure.CreateLocalEvoBoxFolderStructure(folderSelector.SelectedNodes,basePath);
                EvoBoxFolders = boxFolders;
                List<EvoBoxFolder> flatList = new List<EvoBoxFolder>();
                foreach (var f in boxFolders)
                {
                    var flatted = f.Flatten(x => x.ChildFolders);
                    flatList.AddRange(flatted);
                }
                button_CreateBoxFolders.Enabled = true;
                richTextBox1.Text = string.Join("\n", flatList.Where(a=>a.Checked).Select(x => x.FullPath).ToArray());
            }
        }
        
        #endregion

        #region Create Box Folders
        private void button_CreateBoxFolders_Click(object sender, EventArgs e)
        {
            CreateBoxFolders();
        }
        private void CreateBoxFolders()
        {
            folderManager.CreateNewBoxFolderStructure
                (EvoBoxFolders,
                textBox_ClientId.Text,
                textBox_JobId.Text);
        }
        #endregion
    }
}
