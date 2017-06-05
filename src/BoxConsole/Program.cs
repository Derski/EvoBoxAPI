using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvoBoxAPI;
using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace BoxConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TestIt();

            //timer
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5000);
            }
        }
        private static async void TestIt()
        {
            FolderManager folderManager = new FolderManager();
            var testFolders = CreateTestFolderStructure.GetSampleLocalFolders();

            //A folder structure should only be created once per job ID and left alone
            //var foldersList = folderManager.CreateNewBoxFolderStructure(testFolders);

            folderManager.GetBoxFolderIdsForFileFolders(testFolders);


            folderManager.ReadInFolderFiles(testFolders);

            //next upload all the files
            folderManager.UploadAllFiles(testFolders);
        }
        
    }

}
