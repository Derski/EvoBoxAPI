using EvoBoxAPILibrary.File_Services;
using System.Collections.Generic;

namespace EvoBoxAPILibrary
{
    public class EvoBoxFolder
    {
        public bool Checked { get; set; }

        public string BoxId { get; set; }
        public string FolderName { get; set; }

        private string _fileFilter;
        public string FileFilter {
            get
            {
                if(_fileFilter == null && CustomNodeTagData != null)
                {
                    _fileFilter = CustomNodeTagData.FileFilter;
                }
                return _fileFilter;
            }
            set
            {
                _fileFilter = value;
            }
        }

        //ClientJobPrefix_FolderName
        public string BoxFolderName { get; set; }

        //local file path
        private string _fullPath;
        public string FullPath
        {
            get
            {
                if(_fullPath==null && CustomNodeTagData != null)
                {
                    _fullPath = CustomNodeTagData.FullFilePath;
                }
                return _fullPath;
            }
            set
            {
                _fullPath = value;
            }
        }

        public bool FolderExistsLocally { get; set; }
        public string BoxParentId { get; set; }

        public EvoBoxFolder Parent { get; set; }

        private List<EvoBoxFolder> _childFolders;
        public List<EvoBoxFolder> ChildFolders
        {
            get
            {
                if (_childFolders == null)
                {
                    _childFolders = new List<EvoBoxFolder>();
                }
                return _childFolders;
            }
            set
            {
                _childFolders = value;
            }
        }

        private List<EvoBoxFile> _fileNames;
        public List<EvoBoxFile> FileNames
        {
            get
            {
                if(_fileNames == null)
                {
                    _fileNames = new List<EvoBoxFile>();
                }
                return _fileNames;
            }
            set
            {
                _fileNames = value;
            }
        }
        public TreeNodeCustomData CustomNodeTagData { get; internal set; }

        #region Constructors
        public EvoBoxFolder(string boxId, string boxFolderName, string boxParentId)
        {
            BoxId = boxId;
            BoxFolderName = boxFolderName;
            BoxParentId = boxParentId;
        }

        public EvoBoxFolder(string fullPath, string folderName, bool isChecked, string fileFilter)
        {
            FolderName = folderName;
            FullPath = fullPath;
            Checked = isChecked;
            FileFilter = fileFilter;
        }
        public EvoBoxFolder(string folderName)
        {
            FolderName = folderName;
        }
        #endregion
    }

}
