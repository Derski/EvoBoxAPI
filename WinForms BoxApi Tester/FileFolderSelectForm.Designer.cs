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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Select = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_Load = new System.Windows.Forms.Button();
            this.checkBox_FoldersOnly = new System.Windows.Forms.CheckBox();
            this.treeFileSelector = new FileFolderSelector.TreeFileSelector();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeFileSelector);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(489, 395);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Files";
            // 
            // button_Select
            // 
            this.button_Select.Location = new System.Drawing.Point(15, 414);
            this.button_Select.Name = "button_Select";
            this.button_Select.Size = new System.Drawing.Size(117, 23);
            this.button_Select.TabIndex = 2;
            this.button_Select.Text = "Select Files";
            this.button_Select.UseVisualStyleBackColor = true;
            this.button_Select.Click += new System.EventHandler(this.button_Select_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(138, 414);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(117, 23);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(261, 414);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(117, 23);
            this.button_Clear.TabIndex = 4;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(384, 414);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(117, 23);
            this.button_Load.TabIndex = 5;
            this.button_Load.Text = "Load Saved";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // checkBox_FoldersOnly
            // 
            this.checkBox_FoldersOnly.AutoSize = true;
            this.checkBox_FoldersOnly.Checked = true;
            this.checkBox_FoldersOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_FoldersOnly.Location = new System.Drawing.Point(15, 444);
            this.checkBox_FoldersOnly.Name = "checkBox_FoldersOnly";
            this.checkBox_FoldersOnly.Size = new System.Drawing.Size(110, 21);
            this.checkBox_FoldersOnly.TabIndex = 6;
            this.checkBox_FoldersOnly.Text = "Folders Only";
            this.checkBox_FoldersOnly.UseVisualStyleBackColor = true;
            // 
            // treeFileSelector
            // 
            this.treeFileSelector.BasePath = null;
            this.treeFileSelector.CheckBoxes = true;
            this.treeFileSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFileSelector.Location = new System.Drawing.Point(3, 18);
            this.treeFileSelector.Name = "treeFileSelector";
            this.treeFileSelector.Size = new System.Drawing.Size(483, 374);
            this.treeFileSelector.TabIndex = 0;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(261, 444);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(117, 23);
            this.button_OK.TabIndex = 7;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(384, 444);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(117, 23);
            this.button_Cancel.TabIndex = 8;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // FileFolderSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 477);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.checkBox_FoldersOnly);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Select);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(528, 593);
            this.MinimumSize = new System.Drawing.Size(528, 493);
            this.Name = "FileFolderSelectForm";
            this.Text = "File Select Form";
            this.Load += new System.EventHandler(this.FileFolderSelectForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeFileSelector treeFileSelector;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.CheckBox checkBox_FoldersOnly;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
    }
}