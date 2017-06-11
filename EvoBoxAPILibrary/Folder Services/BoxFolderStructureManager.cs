using EvoBoxAPILibrary.File_Services;
using Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EvoBoxAPILibrary
{
    public class BoxFolderStructureManager: IBoxFolderStructureManager
    {
        IClientJobInfo _clientJobInfo;
        TreeNodeXMLSerializer xmlSerializer;
        public BoxFolderStructureManager(IClientJobInfo clientJobInfo)
        {
            _clientJobInfo = clientJobInfo;
            xmlSerializer = new TreeNodeXMLSerializer();
        }
        
        #region Create box folder structure from client, job and local folders from the tree nodes  
        public EvoBoxFolder CreateLocalEvoBoxFolderStructureFromTreeNodes
            (TreeNodeCollection nodes,string clientId, string jobId)
        {
            List<EvoBoxFolder> evoBoxFolders = new List<EvoBoxFolder>();

            foreach (TreeNode node in nodes)
            {
                if(node.Checked)
                {
                    //skip the top node, for ex. we only want the job work folder, not the desktop folder
                    var firstBoxNode = node.Nodes[0];
                    var tag = (TreeNodeCustomData)firstBoxNode.Tag;
                    EvoBoxFolder folder = new EvoBoxFolder(firstBoxNode.FullPath, firstBoxNode.Text, firstBoxNode.Checked,tag.FileFilter);
                    evoBoxFolders.Add(folder);
                    AddChildFolders(folder, firstBoxNode.Nodes);
                }
            }

            EvoBoxFolder ClientRootFolder = null;

            if (evoBoxFolders.Count>0)
            {
                //create jobId Folder and Client Folder
                string jobIdPrefix = _clientJobInfo.GetBoxClientJobIdRootFolderName;

                EvoBoxFolder ClientJobPrefixFolder = new EvoBoxFolder(jobIdPrefix);
                ClientJobPrefixFolder.ChildFolders.AddRange(evoBoxFolders);

                ClientRootFolder = new EvoBoxFolder(clientId);
                ClientRootFolder.ChildFolders.Add(ClientJobPrefixFolder);

                ReadInFolderFiles(ClientRootFolder);
            }

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
        public  void ReadInFolderFiles(EvoBoxFolder rootFolder)
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

        #region Create box folder structure from an xml file
        public EvoBoxFolder TransformXMLtoBoxFolderStructure(string folderConfigFile, IClientJobInfo clientInfo)
        {
            if (File.Exists(folderConfigFile))
            {
                string clientId, jobId;
                EvoBoxFolder folder = xmlSerializer.DeserializeXMLToBoxFolder(folderConfigFile, out clientId, out jobId);
                clientInfo.CurrentSelectedClient = clientId;
                clientInfo.CurrentSelectedJobId = jobId;
                while (folder.Parent != null)
                {
                    folder = folder.Parent;
                }

                return TransformLocalBoxFolderStructureToCloudBoxFolderStructure(folder, clientInfo);
            }
            return null;
        }
        public EvoBoxFolder TransformLocalBoxFolderStructureToCloudBoxFolderStructure
            (EvoBoxFolder localFolderStructure, IClientJobInfo clientInfo)
        {
            EvoBoxFolder rootClientFolder = new EvoBoxFolder(clientInfo.CurrentSelectedClient);
            EvoBoxFolder jobIdFolder = new EvoBoxFolder(clientInfo.CurrentSelectedJobId);
            rootClientFolder.ChildFolders.Add(jobIdFolder);
            jobIdFolder.Parent = rootClientFolder;

            //the root folder of the local box folder structure should be tempRoot, 
            //each top directory folder should be ignored and it's children pointed at the jobIdFolder
            foreach (EvoBoxFolder topDirectoryFolder in localFolderStructure.ChildFolders)
            {
                foreach (EvoBoxFolder actualBoxFolders in topDirectoryFolder.ChildFolders)
                {
                    jobIdFolder.ChildFolders.Add(actualBoxFolders);
                    actualBoxFolders.Parent = jobIdFolder;
                }
            }

            return rootClientFolder;
        }

        #endregion

        #region Validate all local folders exist for a box folder structure

        public void VerifyLocalFolderStructureFromBoxFolder(EvoBoxFolder rootClientFolder)
        {
            if(rootClientFolder != null && rootClientFolder.ChildFolders != null)
            {
                //get the job folder
                EvoBoxFolder jobIdFolder = rootClientFolder.ChildFolders.FirstOrDefault();
                foreach(var folder in jobIdFolder.Flatten(f=>f.ChildFolders))
                {
                    folder.FolderExistsLocally =  Directory.Exists(folder.FullPath);
                }
            }
        }
        #endregion
    }

}
