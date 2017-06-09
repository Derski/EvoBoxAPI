using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoBoxAPILibrary
{
    public class TreeNodeCustomData
    {
        public string BasePath { get;  set; }
        public string FileFilter { get; set; }
        public string FullFilePath { get; set; }
        public bool IsDirectory { get;  set; }
        public bool IncludeInBox { get; set; }

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
                    "IsDirectory=" + IsDirectory.ToString() +
                    ","+
                    "IncludeInBox=" + IncludeInBox.ToString();
            }
        }

        public TreeNodeCustomData(string customTagSavedXML)
        {
            var customTagArray = customTagSavedXML.Split(',');

            var basePathArray = customTagArray[0].Split('=');//Basepath
            BasePath = basePathArray[1];

            var fileFilterArray = customTagArray[1].Split('=');//FileFilter
            FileFilter = fileFilterArray[1];

            var fullFilePathArray = customTagArray[2].Split('=');//FullFilepath
            FullFilePath = fullFilePathArray[1];

            var isDirectoryArray = customTagArray[3].Split('=');//IsDirectory
            bool isDirectoryTryParseResult = true;
            bool.TryParse(isDirectoryArray[1], out isDirectoryTryParseResult);
            IsDirectory = isDirectoryTryParseResult;

            var includeInBoxArray = customTagArray[4].Split('=');//IsDirectory
            bool includeInBoxTryParseResult = true;
            bool.TryParse(includeInBoxArray[1], out includeInBoxTryParseResult);
            IncludeInBox = includeInBoxTryParseResult;

        }
        public TreeNodeCustomData()
        {

        }
    }
}
