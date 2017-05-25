using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NtpLibrary
{
    public static class SystemTimeHack
    {
        public static bool CheckAndTryToFixSystemTime()
        {
            bool isOk = false;
            try
            {
                NetworkTime nt = new NetworkTime();
                var networkTime = nt.GetDateTime();
                var currentSysTime = DateTime.Now;
                var timeDiff = networkTime - currentSysTime;

                if (Math.Abs(timeDiff.TotalSeconds) > 30)
                {
                    isOk = SystemClockSetter.SetTime(networkTime);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return isOk;
        }
    }
}
