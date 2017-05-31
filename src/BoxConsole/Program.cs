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
            FindFolderByClient();
        }

        private static void FindFolderByClient()
        {
            var isOK = NtpLibrary.SystemTimeHack.CheckAndTryToFixSystemTime();
            if (isOK)
            {
                Task<BoxCollection<BoxItem>> task = BoxService.FindFoldersByClient("Phoenix");
                var awaiter = task.GetAwaiter();

                awaiter.OnCompleted(() => OnFindFolderComplete(awaiter.GetResult()));

                //timer
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(5000);
                }
            }
        }

        private static void TestFolderCreate()
        {
            var isOK = NtpLibrary.SystemTimeHack.CheckAndTryToFixSystemTime();
            if (isOK)
            {
                Task<BoxFolder> task = BoxService.CreateFolder("", "");
                var awaiter = task.GetAwaiter();

                awaiter.OnCompleted(() => OnFolderCreatedComplete(awaiter.GetResult()));

                //timer
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(5000);
                }
            }
        }

        private static void TestTimeHack()
        {
            var isOK = NtpLibrary.SystemTimeHack.CheckAndTryToFixSystemTime();
        }

        private static void TestFileCreate()
        {
            Task<BoxFile> task = BoxService.ExecuteMainAsyncFileUpload();
            var awaiter = task.GetAwaiter();

            awaiter.OnCompleted(() => OnFileCreatedComplete(awaiter.GetResult()));

            //timer
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5000);
            }
        }

        private static void TestJWT()
        {
            try
            {
                if(NtpLibrary.SystemTimeHack.CheckAndTryToFixSystemTime())
                {
                    Task t = BoxService.JWTAuthAsync();
                    t.Wait();

                    Console.WriteLine();
                    Console.Write("Press return to exit...");
                    Console.ReadLine();
                }

            }
            catch (Exception)
            {

                throw;
            }


        }

        private static void OnFileCreatedComplete(BoxFile f)
        {

        }
        private static void OnFolderCreatedComplete(BoxFolder f)
        {

        }
        private static void OnFindFolderComplete(BoxCollection<BoxItem> foldersCollection)
        {
            string baseFolderPath = @"C:\Users\derski\Desktop\JobWork\ConsoleMessage";
            FolderManager folderManager = new FolderManager();
            var foldersList =  folderManager.BuildFolderStructure(foldersCollection);
        }
    }
}
