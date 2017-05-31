using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxConsole
{
    public class EvoBoxFolder
    {
        public string BoxId { get; set; }
        public string FolderName { get; set; }

        public string BoxFolderName { get; set; }

        public string FullPath { get; set; }

        public string BoxParentId { get; set; }

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

        private List<string> _fileNames;
        public List<string> FileNames
        {
            get
            {
                if(_fileNames == null)
                {
                    _fileNames = new List<string>();
                }
                return _fileNames;
            }
            set
            {
                _fileNames = value;
            }
        }

        #region Constructors
        public EvoBoxFolder(string boxId, string boxFolderName, string boxParentId)
        {
            BoxId = boxId;
            BoxFolderName = boxFolderName;
            BoxParentId = boxParentId;
        }

        public EvoBoxFolder(string fullPath, string folderName)
        {
            FolderName = folderName;
            FullPath = fullPath;
        }
        public EvoBoxFolder(string folderName)
        {
            FolderName = FolderName;
        }
        #endregion
    }

}
