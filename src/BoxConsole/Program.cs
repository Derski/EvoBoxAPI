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
            Task<BoxFile> task = BoxService.ExecuteMainAsyncFileUpload();
            var awaiter = task.GetAwaiter();
            
            awaiter.OnCompleted(()=> OnFileCreatedComplete(awaiter.GetResult()));

            //timer
            for(int i = 0; i<100;i++)
            {
                Thread.Sleep(5000);
            }
        }

        private static void OnFileCreatedComplete(BoxFile f)
        {

        }
    }
}
