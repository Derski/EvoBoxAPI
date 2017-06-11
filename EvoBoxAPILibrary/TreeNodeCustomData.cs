namespace EvoBoxAPILibrary
{
    public class TreeNodeCustomData
    {
        public string BasePath { get;  set; }
        public string FileFilter { get; set; }
        public string FullFilePath { get; set; }
        public bool IsDirectory { get;  set; }
        public bool IncludeInBox { get; set; }

        public bool IsChecked { get; set; }

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
                    "IncludeInBox=" + IncludeInBox.ToString() +
                    "," +
                    "IsChecked=" + IsChecked.ToString();
            }
        }

        public TreeNodeCustomData(string customTagSavedXML)
        {
            try
            {
                var customTagArray = customTagSavedXML.Split(',');
                
                if(customTagArray.Length==6)
                {
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

                    var includeInBoxArray = customTagArray[4].Split('=');//IncludeInBox
                    bool includeInBoxTryParseResult = true;
                    bool.TryParse(includeInBoxArray[1], out includeInBoxTryParseResult);
                    IncludeInBox = includeInBoxTryParseResult;

                    var isCheckedArray = customTagArray[5].Split('=');//IncludeInBox
                    bool isCheckedTryParseResult = true;
                    bool.TryParse(isCheckedArray[1], out isCheckedTryParseResult);
                    IsChecked = isCheckedTryParseResult;
                }


            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Error parsing custom tag string"+ex.Message);
            }


        }
        public TreeNodeCustomData()
        {

        }
    }
}
