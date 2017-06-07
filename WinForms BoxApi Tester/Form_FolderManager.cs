using EvoBoxAPILibrary;
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
using EvoBoxAPILibrary;
using Box.V2;

namespace WinForms_BoxApi_Tester
{
    public partial class Form_FolderManager : Form
    {
        FolderManager folderManager;

       public EvoBoxFolder EvoBoxFolder { get; set; }

        public Form_FolderManager()
        {
            InitializeComponent();
            BoxClient boxClient =  EvoBoxService.GetAdminClient();
            folderManager = new FolderManager(boxClient);
            textBox_AdminToken.Text = folderManager.AdminToken;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
           
        }

        private void textBox_JobId_TextChanged(object sender, EventArgs e)
        {
           
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
                richTextBox_BoxNodes.Clear();
                IndentPrintFolders(EvoBoxFolder,"",richTextBox_BoxNodes,false); 
            }
        }
        
        private void FolderInfoDisplayLogic(EvoBoxFolder folder, string tab, RichTextBox richTextBox)
        {
            //Display information logic
            if (folder.BoxId != null)
            {
                richTextBox.AppendText((tab + folder.FolderName), Color.Green);
               
            }
            else
            {
                richTextBox.AppendText((tab + folder.FolderName), Color.Red);
            }
        }

        private void IndentPrintFolders(EvoBoxFolder folder, 
            string tab, 
            RichTextBox richTextBox, 
            bool displayBoxInfo
            )
        {
            if(displayBoxInfo)
            {
                FolderInfoDisplayLogic(folder, tab,richTextBox);
            }
            else
            {
                richTextBox.AppendText((tab + folder.FolderName));
            }
            if(folder.FileFilter != null)
            {
                richTextBox.AppendText(" (" + folder.FileFilter + ") ");
            }
            

            //indent children
            richTextBox.AppendText(Environment.NewLine);
            tab += "   ";
            foreach(var childFolder in folder.ChildFolders)
            {
                IndentPrintFolders(childFolder, tab, richTextBox, displayBoxInfo);
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
                (EvoBoxFolder,
                textBox_ClientId.Text,
                textBox_JobId.Text);
            button_UploadFiles.Enabled = true;
            ValidatePrerequisites();
        }
        #endregion

        #region Validate
        private void button_Validate_Click(object sender, EventArgs e)
        {
            ValidatePrerequisites();
        }

        private void ValidatePrerequisites()
        {
            var Errors = ValidateBeforeBoxAction();
            if (Errors != null)
            {
                button_CreateBoxFolders.Enabled = false;
                var userErrors = string.Join("\n", Errors.ToArray());
                MessageBox.Show(userErrors, "Box Folder Errors");
            }
            else
            {
                folderManager.FindFromClientRootAndPopulateBoxAttributes(EvoBoxFolder);
                richTextBox_BoxNodes.Clear();
                IndentPrintFolders(EvoBoxFolder, "", richTextBox_BoxNodes, true);
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
                Errors.Add("Please fix the following issues before attempting to create the box folder structure.");
                if (!adminToken)
                {
                    Errors.Add("Get An Admin Token.");
                }
                if (!clientId)
                {
                    Errors.Add("Set the Client Id.");
                }
                if (!jobId)
                {
                    Errors.Add("Set the Job Id.");
                }
                if (!boxFolders)
                {
                    Errors.Add("Select folders to be created in the box account.");
                }
                return Errors;
            }
            return null;
        }

        #endregion Validate

        #region Upload Files
        private void button_UploadFiles_Click(object sender, EventArgs e)
        {
            folderManager.ReadInFolderFiles(EvoBoxFolder);
            folderManager.UploadAllFiles(EvoBoxFolder);
        }
        #endregion

        #region Client Job Info Interface
        private void button_GetClientJobInfo_Click(object sender, EventArgs e)
        {
            GetClientJobInfo();
        }
        private void GetClientJobInfo()
        {
            IClientJobInfo clientJobInfo = new ClientJobInfoStub();
            Form_ClientJobInfo clientInfoForm =  new Form_ClientJobInfo(clientJobInfo);

            var result = clientInfoForm.ShowDialog();
            if(result.Equals(DialogResult.OK))
            {
                textBox_ClientId.Text = clientInfoForm.ClientId;
                textBox_JobId.Text = clientInfoForm.JobId;
                EvoBoxFolder = null;
                richTextBox_BoxNodes.Clear();
            }
        }
        #endregion
    }

    #region Color Rich Text Extension
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
    #endregion Color Rich Text Extension
}
