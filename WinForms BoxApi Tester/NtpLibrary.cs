using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NtpLibrary
{
    public static class SystemTimeHack
    {
        public static bool CheckAndTryToFixSystemTime()
        {
            bool isOk = true;
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
                isOk = false;
                throw ex;
            }

            return isOk;
        }
    }
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
            var hourOffset = (utc - nt).Hours;

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

            var isOK = Win32SetSystemTime(ref st); // invoke this method.

            return isOK;
        }
    }
    public class NoServerFoundException : System.Exception
    {
        public NoServerFoundException() : base() { }
        public NoServerFoundException(string message) : base(message) { }
        public NoServerFoundException(string message,
                System.Exception inner) : base(message, inner) { }
        protected NoServerFoundException(SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }

    public class NetworkTime
    {
        /* For more info, see:
         *  NTP (RFC-2030)
         *  http://tools.ietf.org/html/rfc2030
         */

        private const int requestTimeout = 3000;
        private const int timesForEachServer = 5;
        private const byte offTime = 40; //Transmit Time (see RFC-2030)
        private uint lastSrv;

        //NIST Servers
        public static string[] srvs = {
        "time.nist.gov",
        "pool.ntp.org",
        "ntp1.inrim.it",
        "ntp2.inrim.it"
    };

        public NetworkTime()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            lastSrv = (uint)rnd.Next(0, srvs.Length);
        }

        //private async Task<IPHostEntry> getHostEntry()
        //{
        //    lastSrv = (uint)((lastSrv + 1) % srvs.Length);
        //    IPHostEntry h = await Dns.GetHostEntryAsync(srvs[lastSrv]);

        //    return h;
        //}

        private  IPHostEntry getHostEntry()
        {
            lastSrv = (uint)((lastSrv + 1) % srvs.Length);
            IPHostEntry h = Dns.GetHostEntry(srvs[lastSrv]);

            return h;
        }

        private IPAddress getServer()
        {
            lastSrv = (uint)((lastSrv + 1) % srvs.Length);

            //IPHostEntry h = getHostEntry().Result;// this is for the async result
            IPHostEntry h = getHostEntry();
            IPAddress[] address = h.AddressList;
            if (address == null || address.Length == 0)
                throw new NoServerFoundException("no ip found");
            return address[0];
        }

        public DateTime GetDateTime() { return GetDateTime(false); }
        public DateTime GetDateTime(bool utc)
        {
            //Examine all servers until we find a server that responds
            for (int st = 0; st < srvs.Length * timesForEachServer; st++)
            {
                try
                {
                    IPAddress ip = getServer();
                    IPEndPoint ipEndP = new IPEndPoint(ip, 123);

                    Socket sk = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Dgram,
                                          ProtocolType.Udp);
                    sk.ReceiveTimeout = requestTimeout;

                    sk.Connect(ipEndP);

                    /* Request
                     * VN: 4 = NTP/SNTP version 4
                     * Mode: 3 = client
                     */
                    byte[] data = new byte[48];
                    data[0] = 0x23;
                    for (int i = 1; i < 48; i++) data[i] = 0;
                    sk.Send(data);

                    /* Response
                     * we read the integer part and fraction part
                     * of transmit time (see RFC-2030)
                     */
                    sk.Receive(data);
                    byte[] integerPart = new byte[4];
                    integerPart[0] = data[offTime + 3];
                    integerPart[1] = data[offTime + 2];
                    integerPart[2] = data[offTime + 1];
                    integerPart[3] = data[offTime + 0];
                    byte[] fractPart = new byte[4];
                    fractPart[0] = data[offTime + 7];
                    fractPart[1] = data[offTime + 6];
                    fractPart[2] = data[offTime + 5];
                    fractPart[3] = data[offTime + 4];
                    long ms = (long)(
                                (ulong)BitConverter.ToUInt32(integerPart, 0) * 1000
                             + ((ulong)BitConverter.ToUInt32(fractPart, 0) * 1000)
                                / 0x100000000L);
                    sk.Shutdown(SocketShutdown.Both);

                    /* DateTime*/
                    DateTime date = new DateTime(1900, 1, 1);
                    date += TimeSpan.FromTicks(ms * TimeSpan.TicksPerMillisecond);

                    return utc ? date : date.ToLocalTime();

                }
                catch (Exception ex) { }
            }

            throw new NoServerFoundException("no working server has been found");
        }
    }
}
