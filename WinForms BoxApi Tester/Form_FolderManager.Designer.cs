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
            this.textBox_AdminToken = new System.Windows.Forms.TextBox();
            this.groupBox_FolderManager = new System.Windows.Forms.GroupBox();
            this.button_BoxAccountInfo = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_UploadFiles = new System.Windows.Forms.Button();
            this.button_Validate = new System.Windows.Forms.Button();
            this.richTextBox_BoxNodes = new System.Windows.Forms.RichTextBox();
            this.label_LocalFolderSelection = new System.Windows.Forms.Label();
            this.button_LocalFolderSelection = new System.Windows.Forms.Button();
            this.label_ClientJobBoxFolders = new System.Windows.Forms.Label();
            this.button_CreateBoxFolders = new System.Windows.Forms.Button();
            this.textBox_JobId = new System.Windows.Forms.TextBox();
            this.textBox_ClientId = new System.Windows.Forms.TextBox();
            this.labelJobId = new System.Windows.Forms.Label();
            this.labelClientId = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_AdminToken = new System.Windows.Forms.Label();
            this.button_Green = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label_Green = new System.Windows.Forms.Label();
            this.label_Red = new System.Windows.Forms.Label();
            this.button_GetClientJobInfo = new System.Windows.Forms.Button();
            this.groupBox_FolderManager.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_AdminToken
            // 
            this.textBox_AdminToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_AdminToken.Location = new System.Drawing.Point(105, 4);
            this.textBox_AdminToken.Name = "textBox_AdminToken";
            this.textBox_AdminToken.ReadOnly = true;
            this.textBox_AdminToken.Size = new System.Drawing.Size(652, 22);
            this.textBox_AdminToken.TabIndex = 1;
            // 
            // groupBox_FolderManager
            // 
            this.groupBox_FolderManager.Controls.Add(this.button_GetClientJobInfo);
            this.groupBox_FolderManager.Controls.Add(this.statusStrip1);
            this.groupBox_FolderManager.Controls.Add(this.groupBox1);
            this.groupBox_FolderManager.Controls.Add(this.textBox_JobId);
            this.groupBox_FolderManager.Controls.Add(this.textBox_ClientId);
            this.groupBox_FolderManager.Controls.Add(this.labelJobId);
            this.groupBox_FolderManager.Controls.Add(this.labelClientId);
            this.groupBox_FolderManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_FolderManager.Location = new System.Drawing.Point(3, 43);
            this.groupBox_FolderManager.Name = "groupBox_FolderManager";
            this.groupBox_FolderManager.Size = new System.Drawing.Size(900, 568);
            this.groupBox_FolderManager.TabIndex = 2;
            this.groupBox_FolderManager.TabStop = false;
            // 
            // button_BoxAccountInfo
            // 
            this.button_BoxAccountInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_BoxAccountInfo.Location = new System.Drawing.Point(769, 4);
            this.button_BoxAccountInfo.Name = "button_BoxAccountInfo";
            this.button_BoxAccountInfo.Size = new System.Drawing.Size(122, 23);
            this.button_BoxAccountInfo.TabIndex = 7;
            this.button_BoxAccountInfo.Text = "Box Account Info";
            this.button_BoxAccountInfo.UseVisualStyleBackColor = true;
            this.button_BoxAccountInfo.Click += new System.EventHandler(this.button_BoxAccountInfo_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(3, 543);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(894, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label_Red);
            this.groupBox1.Controls.Add(this.label_Green);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button_Green);
            this.groupBox1.Controls.Add(this.button_UploadFiles);
            this.groupBox1.Controls.Add(this.button_Validate);
            this.groupBox1.Controls.Add(this.richTextBox_BoxNodes);
            this.groupBox1.Controls.Add(this.label_LocalFolderSelection);
            this.groupBox1.Controls.Add(this.button_LocalFolderSelection);
            this.groupBox1.Controls.Add(this.label_ClientJobBoxFolders);
            this.groupBox1.Controls.Add(this.button_CreateBoxFolders);
            this.groupBox1.Location = new System.Drawing.Point(9, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(882, 476);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folder Structure";
            // 
            // button_UploadFiles
            // 
            this.button_UploadFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_UploadFiles.Enabled = false;
            this.button_UploadFiles.Location = new System.Drawing.Point(466, 447);
            this.button_UploadFiles.Name = "button_UploadFiles";
            this.button_UploadFiles.Size = new System.Drawing.Size(101, 23);
            this.button_UploadFiles.TabIndex = 12;
            this.button_UploadFiles.Text = "Upload Files";
            this.button_UploadFiles.UseVisualStyleBackColor = true;
            this.button_UploadFiles.Click += new System.EventHandler(this.button_UploadFiles_Click);
            // 
            // button_Validate
            // 
            this.button_Validate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Validate.Location = new System.Drawing.Point(267, 447);
            this.button_Validate.Name = "button_Validate";
            this.button_Validate.Size = new System.Drawing.Size(75, 23);
            this.button_Validate.TabIndex = 11;
            this.button_Validate.Text = "Validate";
            this.button_Validate.UseVisualStyleBackColor = true;
            this.button_Validate.Click += new System.EventHandler(this.button_Validate_Click);
            // 
            // richTextBox_BoxNodes
            // 
            this.richTextBox_BoxNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_BoxNodes.Location = new System.Drawing.Point(12, 43);
            this.richTextBox_BoxNodes.Name = "richTextBox_BoxNodes";
            this.richTextBox_BoxNodes.Size = new System.Drawing.Size(864, 376);
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
            this.label_ClientJobBoxFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_ClientJobBoxFolders.AutoSize = true;
            this.label_ClientJobBoxFolders.Location = new System.Drawing.Point(12, 450);
            this.label_ClientJobBoxFolders.Name = "label_ClientJobBoxFolders";
            this.label_ClientJobBoxFolders.Size = new System.Drawing.Size(249, 17);
            this.label_ClientJobBoxFolders.TabIndex = 7;
            this.label_ClientJobBoxFolders.Text = "Create Client Job Box Folder Structure";
            // 
            // button_CreateBoxFolders
            // 
            this.button_CreateBoxFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_CreateBoxFolders.Enabled = false;
            this.button_CreateBoxFolders.Location = new System.Drawing.Point(348, 447);
            this.button_CreateBoxFolders.Name = "button_CreateBoxFolders";
            this.button_CreateBoxFolders.Size = new System.Drawing.Size(112, 23);
            this.button_CreateBoxFolders.TabIndex = 6;
            this.button_CreateBoxFolders.Text = "Create Folders";
            this.button_CreateBoxFolders.UseVisualStyleBackColor = true;
            this.button_CreateBoxFolders.Click += new System.EventHandler(this.button_CreateBoxFolders_Click);
            // 
            // textBox_JobId
            // 
            this.textBox_JobId.Location = new System.Drawing.Point(344, 12);
            this.textBox_JobId.Name = "textBox_JobId";
            this.textBox_JobId.ReadOnly = true;
            this.textBox_JobId.Size = new System.Drawing.Size(193, 22);
            this.textBox_JobId.TabIndex = 3;
            this.textBox_JobId.Text = "Test_123";
            this.textBox_JobId.TextChanged += new System.EventHandler(this.textBox_JobId_TextChanged);
            // 
            // textBox_ClientId
            // 
            this.textBox_ClientId.Location = new System.Drawing.Point(77, 12);
            this.textBox_ClientId.Name = "textBox_ClientId";
            this.textBox_ClientId.ReadOnly = true;
            this.textBox_ClientId.Size = new System.Drawing.Size(193, 22);
            this.textBox_ClientId.TabIndex = 2;
            this.textBox_ClientId.Text = "Phoenix";
            this.textBox_ClientId.TextChanged += new System.EventHandler(this.textBox_ClientId_TextChanged);
            // 
            // labelJobId
            // 
            this.labelJobId.AutoSize = true;
            this.labelJobId.Location = new System.Drawing.Point(276, 15);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(906, 614);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_BoxAccountInfo);
            this.panel1.Controls.Add(this.label_AdminToken);
            this.panel1.Controls.Add(this.textBox_AdminToken);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 34);
            this.panel1.TabIndex = 0;
            // 
            // label_AdminToken
            // 
            this.label_AdminToken.AutoSize = true;
            this.label_AdminToken.Location = new System.Drawing.Point(9, 8);
            this.label_AdminToken.Name = "label_AdminToken";
            this.label_AdminToken.Size = new System.Drawing.Size(52, 17);
            this.label_AdminToken.TabIndex = 2;
            this.label_AdminToken.Text = "Token:";
            // 
            // button_Green
            // 
            this.button_Green.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Green.BackColor = System.Drawing.Color.Green;
            this.button_Green.Location = new System.Drawing.Point(14, 425);
            this.button_Green.Margin = new System.Windows.Forms.Padding(0);
            this.button_Green.Name = "button_Green";
            this.button_Green.Size = new System.Drawing.Size(21, 19);
            this.button_Green.TabIndex = 13;
            this.button_Green.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(130, 425);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 19);
            this.button1.TabIndex = 14;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label_Green
            // 
            this.label_Green.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Green.AutoSize = true;
            this.label_Green.Location = new System.Drawing.Point(38, 427);
            this.label_Green.Name = "label_Green";
            this.label_Green.Size = new System.Drawing.Size(86, 17);
            this.label_Green.TabIndex = 15;
            this.label_Green.Text = "Exists in Box";
            // 
            // label_Red
            // 
            this.label_Red.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Red.AutoSize = true;
            this.label_Red.Location = new System.Drawing.Point(157, 427);
            this.label_Red.Name = "label_Red";
            this.label_Red.Size = new System.Drawing.Size(116, 17);
            this.label_Red.TabIndex = 16;
            this.label_Red.Text = "Not Found in Box";
            // 
            // button_GetClientJobInfo
            // 
            this.button_GetClientJobInfo.Location = new System.Drawing.Point(543, 12);
            this.button_GetClientJobInfo.Name = "button_GetClientJobInfo";
            this.button_GetClientJobInfo.Size = new System.Drawing.Size(93, 23);
            this.button_GetClientJobInfo.TabIndex = 10;
            this.button_GetClientJobInfo.Text = "Job Info";
            this.button_GetClientJobInfo.UseVisualStyleBackColor = true;
            this.button_GetClientJobInfo.Click += new System.EventHandler(this.button_GetClientJobInfo_Click);
            // 
            // Form_FolderManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 614);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(924, 659);
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
        private System.Windows.Forms.TextBox textBox_AdminToken;
        private System.Windows.Forms.GroupBox groupBox_FolderManager;
        private System.Windows.Forms.Button button_BoxAccountInfo;
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
        private System.Windows.Forms.Label label_AdminToken;
        private System.Windows.Forms.Label label_Red;
        private System.Windows.Forms.Label label_Green;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_Green;
        private System.Windows.Forms.Button button_GetClientJobInfo;
    }
}

