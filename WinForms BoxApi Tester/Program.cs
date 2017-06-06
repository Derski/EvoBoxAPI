using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EvoBoxAPI;
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
            string filePath = @"C:\Users\cderkowski\Desktop\JobWork\ConsoleMessage\ConsoleMessageLog - Copy.txt";

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (var cryptoProvider = new SHA1CryptoServiceProvider())
                {
                    string hash = BitConverter
                            .ToString(cryptoProvider.ComputeHash(fs));
                    var anotherHash = cryptoProvider.ComputeHash(fs);
                    string hexHash = hash.Replace("-", "");
                    var boxHash = "bad03c34a82a0ff7acda02622818880ea22b047b";
                    if(boxHash.ToLower().Equals(hexHash.ToLower()))
                    {
                        //YES
                    }
                    //do something with hash
                }
            }
            
        }

    }

    
}
