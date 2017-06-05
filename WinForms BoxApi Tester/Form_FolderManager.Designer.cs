namespace WinForms_BoxApi_Tester
{
    partial class Form_FolderManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_GetToken = new System.Windows.Forms.Button();
            this.textBox_AdminToken = new System.Windows.Forms.TextBox();
            this.groupBox_FolderManager = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox_BoxNodes = new System.Windows.Forms.RichTextBox();
            this.label_LocalFolderSelection = new System.Windows.Forms.Label();
            this.button_LocalFolderSelection = new System.Windows.Forms.Button();
            this.label_ClientJobBoxFolders = new System.Windows.Forms.Label();
            this.button_CreateBoxFolders = new System.Windows.Forms.Button();
            this.textBox_Prefix = new System.Windows.Forms.TextBox();
            this.label_BoxFolderPrefix = new System.Windows.Forms.Label();
            this.textBox_JobId = new System.Windows.Forms.TextBox();
            this.textBox_ClientId = new System.Windows.Forms.TextBox();
            this.labelJobId = new System.Windows.Forms.Label();
            this.labelClientId = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_BoxAccountInfo = new System.Windows.Forms.Button();
            this.button_Validate = new System.Windows.Forms.Button();
            this.button_UploadFiles = new System.Windows.Forms.Button();
            this.groupBox_FolderManager.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_GetToken
            // 
            this.button_GetToken.Location = new System.Drawing.Point(3, 3);
            this.button_GetToken.Name = "button_GetToken";
            this.button_GetToken.Size = new System.Drawing.Size(96, 23);
            this.button_GetToken.TabIndex = 0;
            this.button_GetToken.Text = "Get Token";
            this.button_GetToken.UseVisualStyleBackColor = true;
            this.button_GetToken.Click += new System.EventHandler(this.button_GetToken_Click);
            // 
            // textBox_AdminToken
            // 
            this.textBox_AdminToken.Location = new System.Drawing.Point(105, 4);
            this.textBox_AdminToken.Name = "textBox_AdminToken";
            this.textBox_AdminToken.ReadOnly = true;
            this.textBox_AdminToken.Size = new System.Drawing.Size(786, 22);
            this.textBox_AdminToken.TabIndex = 1;
            // 
            // groupBox_FolderManager
            // 
            this.groupBox_FolderManager.Controls.Add(this.button_BoxAccountInfo);
            this.groupBox_FolderManager.Controls.Add(this.statusStrip1);
            this.groupBox_FolderManager.Controls.Add(this.groupBox1);
            this.groupBox_FolderManager.Controls.Add(this.textBox_Prefix);
            this.groupBox_FolderManager.Controls.Add(this.label_BoxFolderPrefix);
            this.groupBox_FolderManager.Controls.Add(this.textBox_JobId);
            this.groupBox_FolderManager.Controls.Add(this.textBox_ClientId);
            this.groupBox_FolderManager.Controls.Add(this.labelJobId);
            this.groupBox_FolderManager.Controls.Add(this.labelClientId);
            this.groupBox_FolderManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_FolderManager.Location = new System.Drawing.Point(3, 43);
            this.groupBox_FolderManager.Name = "groupBox_FolderManager";
            this.groupBox_FolderManager.Size = new System.Drawing.Size(900, 637);
            this.groupBox_FolderManager.TabIndex = 2;
            this.groupBox_FolderManager.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(3, 612);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(894, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_UploadFiles);
            this.groupBox1.Controls.Add(this.button_Validate);
            this.groupBox1.Controls.Add(this.richTextBox_BoxNodes);
            this.groupBox1.Controls.Add(this.label_LocalFolderSelection);
            this.groupBox1.Controls.Add(this.button_LocalFolderSelection);
            this.groupBox1.Controls.Add(this.label_ClientJobBoxFolders);
            this.groupBox1.Controls.Add(this.button_CreateBoxFolders);
            this.groupBox1.Location = new System.Drawing.Point(9, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(882, 314);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folder Structure";
            // 
            // richTextBox_BoxNodes
            // 
            this.richTextBox_BoxNodes.Location = new System.Drawing.Point(12, 43);
            this.richTextBox_BoxNodes.Name = "richTextBox_BoxNodes";
            this.richTextBox_BoxNodes.Size = new System.Drawing.Size(864, 234);
            this.richTextBox_BoxNodes.TabIndex = 10;
            this.richTextBox_BoxNodes.Text = "";
            // 
            // label_LocalFolderSelection
            // 
            this.label_LocalFolderSelection.AutoSize = true;
            this.label_LocalFolderSelection.Location = new System.Drawing.Point(9, 22);
            this.label_LocalFolderSelection.Name = "label_LocalFolderSelection";
            this.label_LocalFolderSelection.Size = new System.Drawing.Size(202, 17);
            this.label_LocalFolderSelection.TabIndex = 9;
            this.label_LocalFolderSelection.Text = "Select Folders to Sync. to BOX";
            // 
            // button_LocalFolderSelection
            // 
            this.button_LocalFolderSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LocalFolderSelection.Location = new System.Drawing.Point(801, 19);
            this.button_LocalFolderSelection.Name = "button_LocalFolderSelection";
            this.button_LocalFolderSelection.Size = new System.Drawing.Size(75, 23);
            this.button_LocalFolderSelection.TabIndex = 8;
            this.button_LocalFolderSelection.Text = "Select";
            this.button_LocalFolderSelection.UseVisualStyleBackColor = true;
            this.button_LocalFolderSelection.Click += new System.EventHandler(this.button_LocalFolderSelection_Click);
            // 
            // label_ClientJobBoxFolders
            // 
            this.label_ClientJobBoxFolders.AutoSize = true;
            this.label_ClientJobBoxFolders.Location = new System.Drawing.Point(12, 286);
            this.label_ClientJobBoxFolders.Name = "label_ClientJobBoxFolders";
            this.label_ClientJobBoxFolders.Size = new System.Drawing.Size(249, 17);
            this.label_ClientJobBoxFolders.TabIndex = 7;
            this.label_ClientJobBoxFolders.Text = "Create Client Job Box Folder Structure";
            // 
            // button_CreateBoxFolders
            // 
            this.button_CreateBoxFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CreateBoxFolders.Enabled = false;
            this.button_CreateBoxFolders.Location = new System.Drawing.Point(348, 283);
            this.button_CreateBoxFolders.Name = "button_CreateBoxFolders";
            this.button_CreateBoxFolders.Size = new System.Drawing.Size(112, 23);
            this.button_CreateBoxFolders.TabIndex = 6;
            this.button_CreateBoxFolders.Text = "Create Folders";
            this.button_CreateBoxFolders.UseVisualStyleBackColor = true;
            this.button_CreateBoxFolders.Click += new System.EventHandler(this.button_CreateBoxFolders_Click);
            // 
            // textBox_Prefix
            // 
            this.textBox_Prefix.Location = new System.Drawing.Point(512, 11);
            this.textBox_Prefix.Name = "textBox_Prefix";
            this.textBox_Prefix.ReadOnly = true;
            this.textBox_Prefix.Size = new System.Drawing.Size(251, 22);
            this.textBox_Prefix.TabIndex = 5;
            // 
            // label_BoxFolderPrefix
            // 
            this.label_BoxFolderPrefix.AutoSize = true;
            this.label_BoxFolderPrefix.Location = new System.Drawing.Point(405, 14);
            this.label_BoxFolderPrefix.Name = "label_BoxFolderPrefix";
            this.label_BoxFolderPrefix.Size = new System.Drawing.Size(101, 17);
            this.label_BoxFolderPrefix.TabIndex = 4;
            this.label_BoxFolderPrefix.Text = "Box Job Prefix:";
            // 
            // textBox_JobId
            // 
            this.textBox_JobId.Location = new System.Drawing.Point(77, 40);
            this.textBox_JobId.Name = "textBox_JobId";
            this.textBox_JobId.Size = new System.Drawing.Size(193, 22);
            this.textBox_JobId.TabIndex = 3;
            this.textBox_JobId.Text = "Test_123";
            this.textBox_JobId.TextChanged += new System.EventHandler(this.textBox_JobId_TextChanged);
            // 
            // textBox_ClientId
            // 
            this.textBox_ClientId.Location = new System.Drawing.Point(77, 12);
            this.textBox_ClientId.Name = "textBox_ClientId";
            this.textBox_ClientId.Size = new System.Drawing.Size(193, 22);
            this.textBox_ClientId.TabIndex = 2;
            this.textBox_ClientId.Text = "Phoenix";
            this.textBox_ClientId.TextChanged += new System.EventHandler(this.textBox_ClientId_TextChanged);
            // 
            // labelJobId
            // 
            this.labelJobId.AutoSize = true;
            this.labelJobId.Location = new System.Drawing.Point(9, 43);
            this.labelJobId.Name = "labelJobId";
            this.labelJobId.Size = new System.Drawing.Size(50, 17);
            this.labelJobId.TabIndex = 1;
            this.labelJobId.Text = "Job Id:";
            // 
            // labelClientId
            // 
            this.labelClientId.AutoSize = true;
            this.labelClientId.Location = new System.Drawing.Point(9, 15);
            this.labelClientId.Name = "labelClientId";
            this.labelClientId.Size = new System.Drawing.Size(62, 17);
            this.labelClientId.TabIndex = 0;
            this.labelClientId.Text = "Client Id:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox_FolderManager, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(906, 683);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_GetToken);
            this.panel1.Controls.Add(this.textBox_AdminToken);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 34);
            this.panel1.TabIndex = 0;
            // 
            // button_BoxAccountInfo
            // 
            this.button_BoxAccountInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_BoxAccountInfo.Location = new System.Drawing.Point(769, 11);
            this.button_BoxAccountInfo.Name = "button_BoxAccountInfo";
            this.button_BoxAccountInfo.Size = new System.Drawing.Size(122, 23);
            this.button_BoxAccountInfo.TabIndex = 7;
            this.button_BoxAccountInfo.Text = "Box Account Info";
            this.button_BoxAccountInfo.UseVisualStyleBackColor = true;
            this.button_BoxAccountInfo.Click += new System.EventHandler(this.button_BoxAccountInfo_Click);
            // 
            // button_Validate
            // 
            this.button_Validate.Location = new System.Drawing.Point(267, 283);
            this.button_Validate.Name = "button_Validate";
            this.button_Validate.Size = new System.Drawing.Size(75, 23);
            this.button_Validate.TabIndex = 11;
            this.button_Validate.Text = "Validate";
            this.button_Validate.UseVisualStyleBackColor = true;
            this.button_Validate.Click += new System.EventHandler(this.button_Validate_Click);
            // 
            // button_UploadFiles
            // 
            this.button_UploadFiles.Enabled = false;
            this.button_UploadFiles.Location = new System.Drawing.Point(466, 283);
            this.button_UploadFiles.Name = "button_UploadFiles";
            this.button_UploadFiles.Size = new System.Drawing.Size(101, 23);
            this.button_UploadFiles.TabIndex = 12;
            this.button_UploadFiles.Text = "Upload Files";
            this.button_UploadFiles.UseVisualStyleBackColor = true;
            this.button_UploadFiles.Click += new System.EventHandler(this.button_UploadFiles_Click);
            // 
            // Form_FolderManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 683);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form_FolderManager";
            this.Text = "Folder Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox_FolderManager.ResumeLayout(false);
            this.groupBox_FolderManager.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_GetToken;
        private System.Windows.Forms.TextBox textBox_AdminToken;
        private System.Windows.Forms.GroupBox groupBox_FolderManager;
        private System.Windows.Forms.Button button_BoxAccountInfo;
        private System.Windows.Forms.TextBox textBox_Prefix;
        private System.Windows.Forms.Label label_BoxFolderPrefix;
        private System.Windows.Forms.TextBox textBox_JobId;
        private System.Windows.Forms.TextBox textBox_ClientId;
        private System.Windows.Forms.Label labelJobId;
        private System.Windows.Forms.Label labelClientId;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_ClientJobBoxFolders;
        private System.Windows.Forms.Button button_CreateBoxFolders;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label_LocalFolderSelection;
        private System.Windows.Forms.Button button_LocalFolderSelection;
        private System.Windows.Forms.RichTextBox richTextBox_BoxNodes;
        private System.Windows.Forms.Button button_Validate;
        private System.Windows.Forms.Button button_UploadFiles;
    }
}

