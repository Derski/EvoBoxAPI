using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace BoxApiConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            string foo = "foobar";
            string result = foo.Replace("", "asdfasdf");
            //TODO change the main method to be async
            BoxSyncRunner sync = new BoxApiConsole.BoxSyncRunner();
            sync.RunSync();
        }


    }
}
