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
    }
}
