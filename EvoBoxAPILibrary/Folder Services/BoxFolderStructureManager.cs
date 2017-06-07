using Box.V2;
using Box.V2.Models;
using Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvoBoxAPILibrary
{
    public class BoxFolderStructureManager: IBoxFolderStructureManager
    {
        string _jobId;
        string _clientId;

        public BoxFolderStructureManager(string clientId, string jobId)
        {
            _jobId = jobId;
            _clientId = clientId;
        }

        public void UpdateClient(string clientId)
        {
            _clientId = clientId;
        }
        public void UpdateJobId(string jobId)
        {
            _jobId = jobId;
        }

        public string  GetBoxClientJobIdPrefix
        {
            get
            {
                return _clientId + "_" + _jobId + "_";
            }
        }
        public string GetBoxClientJobIdRootFolderName
        {
            get
            {
                return _clientId + "_" + _jobId;
            }
        }

        #region Create box folder structure from client, job and local folders  
        public EvoBoxFolder CreateLocalEvoBoxFolderStructure
            (TreeNodeCollection nodes,string clientId, string jobId)
        {
            List<EvoBoxFolder> evoBoxFolders = new List<EvoBoxFolder>();
            foreach (TreeNode node in nodes)
            {
                if(node.Checked)
                {
                    var firstBoxBode = node.Nodes[0];
                    var tag = (TreeNodeCustomData)firstBoxBode.Tag;
                    EvoBoxFolder folder = new EvoBoxFolder(firstBoxBode.FullPath, firstBoxBode.Text, firstBoxBode.Checked,tag.FileFilter);
                    evoBoxFolders.Add(folder);
                    AddChildFolders(folder, firstBoxBode.Nodes);
                }
            }
            //create jobId Folder and Client Folder
            string jobIdPrefix = GetBoxClientJobIdRootFolderName;

            EvoBoxFolder ClientJobPrefixFolder = new EvoBoxFolder(jobIdPrefix);
            ClientJobPrefixFolder.ChildFolders.AddRange(evoBoxFolders);

            EvoBoxFolder ClientRootFolder = new EvoBoxFolder(clientId);
            ClientRootFolder.ChildFolders.Add(ClientJobPrefixFolder);

            ReadInFolderFiles(ClientRootFolder);

            return ClientRootFolder;
        }
        private static void AddChildFolders(EvoBoxFolder parentFolder, TreeNodeCollection nodes)
        {
            foreach(TreeNode childNode in nodes)
            {
                if(childNode.Checked)
                {
                    var tag =  (TreeNodeCustomData)childNode.Tag;
                    EvoBoxFolder childFolder =
                    new EvoBoxFolder(childNode.FullPath, childNode.Text, childNode.Checked,tag.FileFilter);
                    parentFolder.ChildFolders.Add(childFolder);
                    AddChildFolders(childFolder, childNode.Nodes);
                }
            }
            
        }

        #endregion

        #region Validate with Box which local folders or files already exist in the cloud and populate the box IDs and SHA1 hash values
        /// <summary>
        /// Take a local folder structure, connect to the box server and update all box attributes for the given folders and files
        /// This should populate all the box IDs and the sha1 hash to perform a checksum
        /// </summary>
        /// <param name="evoBoxFolder"> The representation of the local folder structure to be synchronized to Box 
        /// the root folder always begins with the Client Name and the immidiate subfolder will be the Job ID
        /// </param>
        public void ReadCloudFolderMetadataLocally
            (EvoBoxFolder evoBoxFolder, FolderManager folderManager, FileManager fileManager)
        {
            //for all folders populate the folder Box IDs
            folderManager.FindFromClientRootAndPopulateBoxAttributes(evoBoxFolder);
            var flat = evoBoxFolder.Flatten(x => x.ChildFolders);
            foreach(EvoBoxFolder folder in evoBoxFolder.Flatten(x => x.ChildFolders).Where(f=>f.BoxId != null))
            {
                fileManager.GetBoxFileInfoForBoxFolders(folder);
            }
            
        }
        #endregion

        #region Read in All the Files in the folder
        private void ReadInFolderFiles(EvoBoxFolder rootFolder)
        {
            foreach (var folder in rootFolder.Flatten(x => x.ChildFolders))
            {
                ReadFilesPerFolder(folder);
            }
        }
        private void ReadFilesPerFolder(EvoBoxFolder folder)
        {
            if (Directory.Exists(folder.FullPath))
            {
                //FILTER
                var fileExtentions = folder.FileFilter.Replace("*.", ".").Split('|').ToList();
                DirectoryInfo directoryInfo = new DirectoryInfo(folder.FullPath);
                foreach (var fileInfo in directoryInfo.GetFiles())
                {
                    var extension = fileInfo.Extension;
                    if (fileExtentions.Contains(extension) || fileExtentions.Contains(".*"))
                    {
                        folder.FileNames.Add(new EvoBoxFile(fileInfo.FullName));
                    }
                }
            }
        }
        #endregion
    }

}
