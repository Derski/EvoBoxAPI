using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_BoxApi_Tester
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            //fileFilterTest("*.*");
            //fileFilterTest("*.txt|*.evoset|*.bmp");
        }

        private static FileInfo[] fileFilterTest(string searchPattern)
        {
            //C:\Users\cderkowski\Desktop\FilterTest
            DirectoryInfo dinfo = new DirectoryInfo(@"C:\Users\cderkowski\Desktop\FilterTest");
            string[] extensions = searchPattern.Replace("*.", ".").Split('|');
            //string[] extensions = new[] { ".evoset", ".txt", ".bmp" };
            if(extensions.Contains(".*"))
            {
                return dinfo.EnumerateFiles().ToArray();
            }

            FileInfo[] files =
                dinfo.EnumerateFiles()
                     .Where(f => extensions.Contains(f.Extension.ToLower()))
                     .ToArray();
            return files;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Join("\n", fileFilterTest(textBox1.Text).Select(f=>f.Name));
        }

        private void button_Validate_Click(object sender, EventArgs e)
        {
            ValidateFileFilter(textBox_Regex.Text);
        }
        private void ValidateFileFilter(string filter)
        {
            //var mypattern =   \*.\w+$(\|\*.\w+)*  
            Regex rgx = new Regex(filter, RegexOptions.IgnoreCase);
            bool isMatch =  rgx.IsMatch(textBox1.Text);
            textBox_IsMatch.Text = isMatch.ToString();
        }
    }
}
