using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EvoBoxAPILibrary;
using EvoBoxAPILibrary;
using Box.V2.Utility;
using System.IO;

namespace WinForms_BoxApi_Tester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_FolderManager());
            //Testit();
        }

        private static void Testit()
        {

            
        }

    }

    
}
