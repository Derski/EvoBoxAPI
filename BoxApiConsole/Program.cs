using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxApiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO change the main method to be async
            BoxSyncRunner sync = new BoxApiConsole.BoxSyncRunner();
            sync.RunSync();
        }
    }
}
