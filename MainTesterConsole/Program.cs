using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            NtpLibrary.NetworkTime ntp = new NtpLibrary.NetworkTime();
            var dt = ntp.GetDateTime();
            var sysDt = DateTime.Now;
        }
    }
}
