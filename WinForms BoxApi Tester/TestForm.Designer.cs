namespace WinForms_BoxApi_Tester
{
    partial class TestForm
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
            this.button_GetFiles = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label_Regex = new System.Windows.Forms.Label();
            this.textBox_Regex = new System.Windows.Forms.TextBox();
            this.button_Validate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_IsMatch = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_GetFiles
            // 
            this.button_GetFiles.Location = new System.Drawing.Point(339, 38);
            this.button_GetFiles.Name = "button_GetFiles";
            this.button_GetFiles.Size = new System.Drawing.Size(75, 23);
            this.button_GetFiles.TabIndex = 0;
            this.button_GetFiles.Text = "GetFiles";
            this.button_GetFiles.UseVisualStyleBackColor = true;
            this.button_GetFiles.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(69, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(264, 22);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "C:\\Users\\cderkowski\\Desktop\\FilterTest";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(16, 149);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(399, 196);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // label_Regex
            // 
            this.label_Regex.AutoSize = true;
            this.label_Regex.Location = new System.Drawing.Point(12, 74);
            this.label_Regex.Name = "label_Regex";
            this.label_Regex.Size = new System.Drawing.Size(54, 17);
            this.label_Regex.TabIndex = 5;
            this.label_Regex.Text = "Pattern";
            // 
            // textBox_Regex
            // 
            this.textBox_Regex.Location = new System.Drawing.Point(69, 74);
            this.textBox_Regex.Name = "textBox_Regex";
            this.textBox_Regex.Size = new System.Drawing.Size(264, 22);
            this.textBox_Regex.TabIndex = 6;
            this.textBox_Regex.Text = "\\*\\.w+";
            // 
            // button_Validate
            // 
            this.button_Validate.Location = new System.Drawing.Point(339, 72);
            this.button_Validate.Name = "button_Validate";
            this.button_Validate.Size = new System.Drawing.Size(75, 23);
            this.button_Validate.TabIndex = 7;
            this.button_Validate.Text = "Validate";
            this.button_Validate.UseVisualStyleBackColor = true;
            this.button_Validate.Click += new System.EventHandler(this.button_Validate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Match";
            // 
            // textBox_IsMatch
            // 
            this.textBox_IsMatch.Location = new System.Drawing.Point(69, 106);
            this.textBox_IsMatch.Name = "textBox_IsMatch";
            this.textBox_IsMatch.Size = new System.Drawing.Size(264, 22);
            this.textBox_IsMatch.TabIndex = 9;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 355);
            this.Controls.Add(this.textBox_IsMatch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_Validate);
            this.Controls.Add(this.textBox_Regex);
            this.Controls.Add(this.label_Regex);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_GetFiles);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_GetFiles;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label_Regex;
        private System.Windows.Forms.TextBox textBox_Regex;
        private System.Windows.Forms.Button button_Validate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_IsMatch;
    }
}