using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;



namespace NtpLibrary
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME
    {
        public short wYear;
        public short wMonth;
        public short wDayOfWeek;
        public short wDay;
        public short wHour;
        public short wMinute;
        public short wSecond;
        public short wMilliseconds;
    }

    public static class SystemClockSetter
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetSystemTime(ref SYSTEMTIME st);

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public extern static bool Win32SetSystemTime(ref SYSTEMTIME sysTime);

        public static bool SetTime(DateTime networkDt)
        {
            var utc = networkDt.ToUniversalTime();
            var nt = networkDt;
            var hourOffset = (utc-nt).Hours;

            nt = utc;

            SYSTEMTIME st = new SYSTEMTIME();
            st.wYear = (short)nt.Year; // must be short
            st.wMonth = (short)nt.Month;
            st.wDayOfWeek = (short)nt.DayOfWeek;
            st.wDay = (short)nt.Day;
            //st.wHour = (short)((nt.Hour+hourOffset)%24);
            st.wHour = (short)nt.Hour;
            st.wMinute = (short)nt.Minute;
            st.wSecond = (short)nt.Second;
            st.wMilliseconds = (short)nt.Millisecond;

            var isOK =  Win32SetSystemTime(ref st); // invoke this method.
            
            return isOK;
        }
    }
}
