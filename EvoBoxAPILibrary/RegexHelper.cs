using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EvoBoxAPILibrary
{
    public static class RegexHelper
    {
        //example:
        //string xmlStartElement = "TreeView Local Folder Structure ";
        //xmlStartElement += "ClientId=" + clientId + ";";
        //xmlStartElement += "JobId=" + jobId;
        public static void ExtractClientAndJobIds(string inputString, out string clientId, out string jobId)
        {
            Regex re = new Regex("ClientId=(?<Part1>\\w+);.*JobId=(?<Part2>.+)");
            Match m = re.Match(inputString);
            clientId = m.Groups["Part1"].Value;
            jobId = m.Groups["Part2"].Value;
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="currentUserName"></param>
        /// <returns></returns>
        public static string ReplaceUserNameForCurrentUser(ref string path, string currentUserName)
        {
            var oldUserName = ExtractUserName(path);
            if(!string.IsNullOrEmpty(oldUserName))
            {
                path = path.Replace(oldUserName, currentUserName);
            }
            return path;
        }

        public static string ExtractUserName(string path)
        {
            string userName = "";
            Regex re = new Regex(@"C:\\Users\\(?<UserName>\w+)\\.*");
            Match m = re.Match(path);
            if(m.Success)
            {
                userName = m.Groups["UserName"].Value;
            }
            return userName;
        }

    }
}
