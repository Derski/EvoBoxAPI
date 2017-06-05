using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoBoxAPI
{
    public class TreeNodeCustomData
    {
        public string BasePath { get;  set; }
        public string FileFilter { get; set; }
        public string FullFilePath { get; set; }
        public bool IsDirectory { get;  set; }

        public string CustomTagAsString
        {
            get
            {
                return "BasePath=" + BasePath +
                    "," +
                    "FileFilter=" + FileFilter +
                    "," +
                    "FullFilePath=" + FullFilePath +
                    "," +
                    "IsDirectory=" + IsDirectory.ToString();
            }
        }

        public TreeNodeCustomData(string customTagSavedXML)
        {
            var customTagArray = customTagSavedXML.Split(',');

            var basePathArray = customTagArray[0].Split('=');//Basepath
            BasePath = basePathArray[1];

            var fileFilterArray = customTagArray[1].Split('=');//FileFilter
            FileFilter = fileFilterArray[1];

            var fullFilePathArray = customTagArray[2].Split('=');//FileFilter
            FullFilePath = fullFilePathArray[1];

            var isDirectoryArray = customTagArray[3].Split('=');//FileFilter
            bool isDirectoryTryParseResult = true;
            bool.TryParse(isDirectoryArray[1], out isDirectoryTryParseResult);
            IsDirectory = isDirectoryTryParseResult;
        }
        public TreeNodeCustomData()
        {

        }
    }
}
