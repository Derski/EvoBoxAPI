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
using Box.V2.Models;

namespace WinForms_BoxApi_Tester
{
    public partial class Form_FolderManager : Form
    {
        FolderManager folderManager;
        public string ClientJobPrefix
        {
            get
            {
                return textBox_ClientId.Text + "_" + textBox_JobId.Text;
            }
        }
       public EvoBoxFolder EvoBoxFolder { get; set; }

        public Form_FolderManager()
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

        #region Select Local Folders
        private void button_LocalFolderSelection_Click(object sender, EventArgs e)
        {
            SelectLocalFolders();
        }
        private void SelectLocalFolders()
        {
            FileFolderSelectForm folderSelector = new FileFolderSelectForm();
            var result =  folderSelector.ShowDialog();
            if(result == DialogResult.OK)
            {
                EvoBoxFolder  = BoxFolderStructure.CreateLocalEvoBoxFolderStructure
                    (folderSelector.SelectedNodes,textBox_ClientId.Text,textBox_JobId.Text);
                richTextBox_BoxNodes.Text =
                        string.Join("\n", EvoBoxFolder.Flatten(x => x.ChildFolders).Select(n => n.FolderName))+"\n";  
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
            folderManager.FindFromClientRootAndPopulateBoxAttributes(EvoBoxFolder);

            folderManager.CreateNewBoxFolderStructure
                (EvoBoxFolder,
                textBox_ClientId.Text,
                textBox_JobId.Text);
            button_UploadFiles.Enabled = true;
        }
        #endregion

        private void button_Validate_Click(object sender, EventArgs e)
        {
            var Errors = ValidateBeforeBoxAction();
            if(Errors!= null)
            {
                button_CreateBoxFolders.Enabled = false;
                var userErrors = string.Join("\n", Errors.ToArray());
                MessageBox.Show(userErrors, "Box Folder Errors");
            }
            else
            {
                button_CreateBoxFolders.Enabled = true;
            }
        }


        private List<string> ValidateBeforeBoxAction()
        {
            var adminToken = !string.IsNullOrEmpty(textBox_AdminToken.Text);
            var clientId = !string.IsNullOrEmpty(textBox_ClientId.Text);
            var jobId = !string.IsNullOrEmpty(textBox_JobId.Text);
            var boxFolders = EvoBoxFolder != null;

            if (adminToken && clientId && jobId && boxFolders)
            {
                
            }
            else
            {
                List<string> Errors = new List<string>();
                Errors.Add("Please fix the following issues before attempting to create the box folder structure.\n");
                if (!adminToken)
                {
                    Errors.Add("Please get An Admin Token.");
                }
                if (!clientId)
                {
                    Errors.Add("Please set the Client Id");
                }
                if (!jobId)
                {
                    Errors.Add("Please set the Job Id.");
                }
                if (!boxFolders)
                {
                    Errors.Add("Please select folders to be created in the box account");
                }
                return Errors;
            }
            return null;
        }

        private void button_UploadFiles_Click(object sender, EventArgs e)
        {
            folderManager.ReadInFolderFiles(EvoBoxFolder);
            folderManager.UploadAllFiles(EvoBoxFolder);
        }
    }
}
