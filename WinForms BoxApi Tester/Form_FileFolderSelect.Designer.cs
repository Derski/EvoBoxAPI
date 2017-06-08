namespace FileFolderSelector
{
    partial class FileFolderSelectForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeFileSelector = new FileFolderSelector.TreeFileSelector();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_HideNodeFiles = new System.Windows.Forms.Button();
            this.label_FolderFilter = new System.Windows.Forms.Label();
            this.label_FolderFilterLbl = new System.Windows.Forms.Label();
            this.button_SetNodeFilter = new System.Windows.Forms.Button();
            this.label_NodeFullPath = new System.Windows.Forms.Label();
            this.label_SelectedNodeFullPath = new System.Windows.Forms.Label();
            this.button_ShowNodeFiles = new System.Windows.Forms.Button();
            this.textBox_SelectedNodeFilter = new System.Windows.Forms.TextBox();
            this.label_FileFilter = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_Load = new System.Windows.Forms.Button();
            this.button_Select = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Clear = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelClientId = new System.Windows.Forms.Label();
            this.textBox_ClientId = new System.Windows.Forms.TextBox();
            this.labelJobId = new System.Windows.Forms.Label();
            this.textBox_JobId = new System.Windows.Forms.TextBox();
            this.button_GetClientJobInfo = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeFileSelector);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 432);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Files";
            // 
            // treeFileSelector
            // 
            this.treeFileSelector.CheckBoxes = true;
            this.treeFileSelector.CurrentSelectedNode = null;
            this.treeFileSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFileSelector.Location = new System.Drawing.Point(3, 18);
            this.treeFileSelector.Name = "treeFileSelector";
            this.treeFileSelector.Size = new System.Drawing.Size(617, 411);
            this.treeFileSelector.TabIndex = 0;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(380, 5);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(117, 23);
            this.button_OK.TabIndex = 7;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(503, 5);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(117, 23);
            this.button_Cancel.TabIndex = 8;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_HideNodeFiles);
            this.groupBox2.Controls.Add(this.label_FolderFilter);
            this.groupBox2.Controls.Add(this.label_FolderFilterLbl);
            this.groupBox2.Controls.Add(this.button_SetNodeFilter);
            this.groupBox2.Controls.Add(this.label_NodeFullPath);
            this.groupBox2.Controls.Add(this.label_SelectedNodeFullPath);
            this.groupBox2.Controls.Add(this.button_ShowNodeFiles);
            this.groupBox2.Controls.Add(this.textBox_SelectedNodeFilter);
            this.groupBox2.Controls.Add(this.label_FileFilter);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 517);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(623, 130);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Node";
            // 
            // button_HideNodeFiles
            // 
            this.button_HideNodeFiles.Location = new System.Drawing.Point(127, 100);
            this.button_HideNodeFiles.Name = "button_HideNodeFiles";
            this.button_HideNodeFiles.Size = new System.Drawing.Size(117, 23);
            this.button_HideNodeFiles.TabIndex = 8;
            this.button_HideNodeFiles.Text = "Hide Files";
            this.button_HideNodeFiles.UseVisualStyleBackColor = true;
            this.button_HideNodeFiles.Click += new System.EventHandler(this.button_HideNodeFiles_Click);
            // 
            // label_FolderFilter
            // 
            this.label_FolderFilter.AutoSize = true;
            this.label_FolderFilter.Location = new System.Drawing.Point(104, 48);
            this.label_FolderFilter.Name = "label_FolderFilter";
            this.label_FolderFilter.Size = new System.Drawing.Size(13, 17);
            this.label_FolderFilter.TabIndex = 7;
            this.label_FolderFilter.Text = "*";
            // 
            // label_FolderFilterLbl
            // 
            this.label_FolderFilterLbl.AutoSize = true;
            this.label_FolderFilterLbl.Location = new System.Drawing.Point(7, 48);
            this.label_FolderFilterLbl.Name = "label_FolderFilterLbl";
            this.label_FolderFilterLbl.Size = new System.Drawing.Size(90, 17);
            this.label_FolderFilterLbl.TabIndex = 6;
            this.label_FolderFilterLbl.Text = "Current Filter";
            // 
            // button_SetNodeFilter
            // 
            this.button_SetNodeFilter.Location = new System.Drawing.Point(407, 72);
            this.button_SetNodeFilter.Name = "button_SetNodeFilter";
            this.button_SetNodeFilter.Size = new System.Drawing.Size(117, 23);
            this.button_SetNodeFilter.TabIndex = 5;
            this.button_SetNodeFilter.Text = "Set";
            this.button_SetNodeFilter.UseVisualStyleBackColor = true;
            this.button_SetNodeFilter.Click += new System.EventHandler(this.button_SetNodeFilter_Click);
            // 
            // label_NodeFullPath
            // 
            this.label_NodeFullPath.AutoSize = true;
            this.label_NodeFullPath.Location = new System.Drawing.Point(104, 21);
            this.label_NodeFullPath.Name = "label_NodeFullPath";
            this.label_NodeFullPath.Size = new System.Drawing.Size(39, 17);
            this.label_NodeFullPath.TabIndex = 4;
            this.label_NodeFullPath.Text = "c:\\\\...";
            // 
            // label_SelectedNodeFullPath
            // 
            this.label_SelectedNodeFullPath.AutoSize = true;
            this.label_SelectedNodeFullPath.Location = new System.Drawing.Point(7, 21);
            this.label_SelectedNodeFullPath.Name = "label_SelectedNodeFullPath";
            this.label_SelectedNodeFullPath.Size = new System.Drawing.Size(67, 17);
            this.label_SelectedNodeFullPath.TabIndex = 3;
            this.label_SelectedNodeFullPath.Text = "Full Path:";
            // 
            // button_ShowNodeFiles
            // 
            this.button_ShowNodeFiles.Location = new System.Drawing.Point(6, 100);
            this.button_ShowNodeFiles.Name = "button_ShowNodeFiles";
            this.button_ShowNodeFiles.Size = new System.Drawing.Size(115, 23);
            this.button_ShowNodeFiles.TabIndex = 2;
            this.button_ShowNodeFiles.Text = "Show Files";
            this.button_ShowNodeFiles.UseVisualStyleBackColor = true;
            this.button_ShowNodeFiles.Click += new System.EventHandler(this.button_ShowNodeFiles_Click);
            // 
            // textBox_SelectedNodeFilter
            // 
            this.textBox_SelectedNodeFilter.Location = new System.Drawing.Point(127, 72);
            this.textBox_SelectedNodeFilter.Name = "textBox_SelectedNodeFilter";
            this.textBox_SelectedNodeFilter.Size = new System.Drawing.Size(274, 22);
            this.textBox_SelectedNodeFilter.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBox_SelectedNodeFilter, "ex \"*.txt|*.evoset|*.csv\"");
            this.textBox_SelectedNodeFilter.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_SelectedNodeFilter_Validating);
            // 
            // label_FileFilter
            // 
            this.label_FileFilter.AutoSize = true;
            this.label_FileFilter.Location = new System.Drawing.Point(8, 75);
            this.label_FileFilter.Name = "label_FileFilter";
            this.label_FileFilter.Size = new System.Drawing.Size(68, 17);
            this.label_FileFilter.TabIndex = 0;
            this.label_FileFilter.Text = "Set Filter:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToFilterToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 30);
            // 
            // addToFilterToolStripMenuItem
            // 
            this.addToFilterToolStripMenuItem.Name = "addToFilterToolStripMenuItem";
            this.addToFilterToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.addToFilterToolStripMenuItem.Text = "Add To Filter";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(629, 689);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_Select);
            this.panel2.Controls.Add(this.button_Save);
            this.panel2.Controls.Add(this.button_Clear);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(623, 30);
            this.panel2.TabIndex = 10;
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(522, 5);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(92, 23);
            this.button_Load.TabIndex = 5;
            this.button_Load.Text = "Load Saved";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // button_Select
            // 
            this.button_Select.Location = new System.Drawing.Point(6, 5);
            this.button_Select.Name = "button_Select";
            this.button_Select.Size = new System.Drawing.Size(92, 23);
            this.button_Select.TabIndex = 2;
            this.button_Select.Text = "Add Folder";
            this.button_Select.UseVisualStyleBackColor = true;
            this.button_Select.Click += new System.EventHandler(this.button_Select_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(522, 5);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(92, 23);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(104, 5);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(92, 23);
            this.button_Clear.TabIndex = 4;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 653);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 33);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_GetClientJobInfo);
            this.panel3.Controls.Add(this.textBox_JobId);
            this.panel3.Controls.Add(this.labelJobId);
            this.panel3.Controls.Add(this.textBox_ClientId);
            this.panel3.Controls.Add(this.labelClientId);
            this.panel3.Controls.Add(this.button_Load);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(623, 34);
            this.panel3.TabIndex = 11;
            // 
            // labelClientId
            // 
            this.labelClientId.AutoSize = true;
            this.labelClientId.Location = new System.Drawing.Point(7, 8);
            this.labelClientId.Name = "labelClientId";
            this.labelClientId.Size = new System.Drawing.Size(62, 17);
            this.labelClientId.TabIndex = 6;
            this.labelClientId.Text = "Client Id:";
            // 
            // textBox_ClientId
            // 
            this.textBox_ClientId.Location = new System.Drawing.Point(75, 5);
            this.textBox_ClientId.Name = "textBox_ClientId";
            this.textBox_ClientId.ReadOnly = true;
            this.textBox_ClientId.Size = new System.Drawing.Size(125, 22);
            this.textBox_ClientId.TabIndex = 7;
            // 
            // labelJobId
            // 
            this.labelJobId.AutoSize = true;
            this.labelJobId.Location = new System.Drawing.Point(206, 8);
            this.labelJobId.Name = "labelJobId";
            this.labelJobId.Size = new System.Drawing.Size(50, 17);
            this.labelJobId.TabIndex = 8;
            this.labelJobId.Text = "Job Id:";
            // 
            // textBox_JobId
            // 
            this.textBox_JobId.Location = new System.Drawing.Point(262, 5);
            this.textBox_JobId.Name = "textBox_JobId";
            this.textBox_JobId.ReadOnly = true;
            this.textBox_JobId.Size = new System.Drawing.Size(139, 22);
            this.textBox_JobId.TabIndex = 9;
            // 
            // button_GetClientJobInfo
            // 
            this.button_GetClientJobInfo.Location = new System.Drawing.Point(423, 5);
            this.button_GetClientJobInfo.Name = "button_GetClientJobInfo";
            this.button_GetClientJobInfo.Size = new System.Drawing.Size(93, 23);
            this.button_GetClientJobInfo.TabIndex = 11;
            this.button_GetClientJobInfo.Text = "Job Info";
            this.button_GetClientJobInfo.UseVisualStyleBackColor = true;
            this.button_GetClientJobInfo.Click += new System.EventHandler(this.button_GetClientJobInfo_Click);
            // 
            // FileFolderSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 689);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(1500, 1000);
            this.MinimumSize = new System.Drawing.Size(581, 693);
            this.Name = "FileFolderSelectForm";
            this.Text = "Folder Structure for: ";
            this.Load += new System.EventHandler(this.FileFolderSelectForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TreeFileSelector treeFileSelector;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_SelectedNodeFullPath;
        private System.Windows.Forms.Button button_ShowNodeFiles;
        private System.Windows.Forms.TextBox textBox_SelectedNodeFilter;
        private System.Windows.Forms.Label label_FileFilter;
        private System.Windows.Forms.Label label_NodeFullPath;
        private System.Windows.Forms.Button button_SetNodeFilter;
        private System.Windows.Forms.Button button_HideNodeFiles;
        private System.Windows.Forms.Label label_FolderFilter;
        private System.Windows.Forms.Label label_FolderFilterLbl;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToFilterToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelClientId;
        private System.Windows.Forms.TextBox textBox_ClientId;
        private System.Windows.Forms.Label labelJobId;
        private System.Windows.Forms.TextBox textBox_JobId;
        private System.Windows.Forms.Button button_GetClientJobInfo;
    }
}