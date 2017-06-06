using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoBoxAPI
{
    public class EvoBoxFolder
    {
        public bool Checked { get; set; }
        public string BoxId { get; set; }
        public string FolderName { get; set; }

        public string FileFilter { get; set; }

        //ClientJobPrefix_FolderName
        public string BoxFolderName { get; set; }

        //local file path
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
