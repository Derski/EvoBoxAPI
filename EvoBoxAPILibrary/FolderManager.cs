using Box.V2;
using Box.V2.Models;
using Box.V2.Services;
using EvoBoxAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Extensions;

namespace EvoBoxAPI
{
    public class FolderManager
    {

        public bool TokenValid
        {
            get
            {
                bool valid = false;

                return valid;
            }
        }

        public string AdminToken
        {
            get
            {
                if(boxClient==null)
                {
                    return "Not Currently Authenticaten";
                }
                else
                {
                    try
                    {
                        return boxClient.Auth.Session.AccessToken;
                    }
                    catch(Exception ex)
                    {
                        return ex.Message;
                    }
                }
                
            }
        }

        BoxClient boxClient;

        #region Constructor
        //Constructor
        public FolderManager()
        {
            boxClient = EvoBoxAPI.EvoBoxService.GetAdminClient();
        }
        #endregion Constructor

        #region Folder Structure Create
        public List<EvoBoxFolder> CreateNewBoxFolderStructure
            (EvoBoxFolder localFolder,
            string clientId,
            string jobId
            )
        {
            BoxFolder clientRootFolder = BoxFolderCreate(clientId, "0", boxClient);
            if(clientRootFolder != null)
            {
                BoxFolder jobRootFolder = BoxFolderCreate(clientId + "_" + jobId, clientRootFolder.Id, boxClient);
                if(jobRootFolder != null)
                {
                    CreateFolderHierarchy(localFolder, jobRootFolder.Id,clientId,jobId);  
                }
            }

            return null;
        }

        private void CreateFolderHierarchy
            (EvoBoxFolder currentFolder,
            string parentFolderId,
            string clientId,
            string jobId)
        {
            
            BoxFolder currentBoxFolder = 
            BoxFolderCreate(clientId + "_" + jobId + "_" + currentFolder.FolderName, parentFolderId, boxClient);
            if(currentBoxFolder != null)
            {
                currentFolder.BoxId = currentBoxFolder.Id;
                currentFolder.BoxParentId = parentFolderId;
                foreach(var childFolder in currentFolder.ChildFolders)
                {
                    CreateFolderHierarchy(childFolder, currentBoxFolder.Id,clientId,jobId);
                }
            }
        }

        private static BoxFolder BoxFolderCreate(string folderName, string parentFolderId, BoxClient client)
        {
            Task<BoxFolder> task = EvoBoxService.CreateFolder(folderName, parentFolderId, client);
            var awaiter = task.GetAwaiter();
            //awaiter.OnCompleted(() => OnFolderCreatedComplete(awaiter.GetResult()));
            return awaiter.GetResult();
        }
        #endregion Folder Structure Create

        #region Find

        public BoxItem FindRootClientFolder(string clientId)
        {
            Task<BoxCollection<BoxItem>> task = EvoBoxService.FindFoldersByKeyword(clientId, boxClient);
            var awaiter = task.GetAwaiter();
            foreach (BoxItem boxFolder in awaiter.GetResult().Entries)
            {
                if(boxFolder.Name.Equals(clientId))
                {
                    return boxFolder;
                }
            }
                return null;
        }

        public BoxItem FindRootJobIdFolder(string parentId, string clientJobPrefix)
        {
            Task<BoxCollection<BoxItem>> task = EvoBoxService.FindFoldersByKeyword(clientJobPrefix, boxClient);
            var awaiter = task.GetAwaiter();
            foreach (BoxItem boxFolder in awaiter.GetResult().Entries)
            {
                if (boxFolder.Parent.Id==parentId)
                {
                    return boxFolder;
                }
            }
            return null;
        }

        #endregion

        #region Map Local Folders to Box Folders on Box Folder IDs
        public void GetBoxFolderIdsForFileFolders(List<EvoBoxFolder> localFolders, string clientJobPrefix)
        {
            Task<BoxCollection<BoxItem>> task = EvoBoxService.FindFoldersByKeyword(clientJobPrefix, boxClient);
            var awaiter = task.GetAwaiter();
            //awaiter.OnCompleted(() => OnFindFolderComplete(awaiter.GetResult()));
            var flattened = Flatten(localFolders);
            foreach (BoxItem boxFolder in awaiter.GetResult().Entries)
            {
                string fileName =  boxFolder.Name.Replace(clientJobPrefix, "");
                var localFolderMatch = flattened.Where(l => l.FolderName == fileName);
                if(localFolderMatch.Count()==1)
                {
                    localFolderMatch.FirstOrDefault().BoxId = boxFolder.Id;
                    localFolderMatch.FirstOrDefault().BoxParentId = boxFolder.Parent.Id;
                    localFolderMatch.FirstOrDefault().BoxFolderName = boxFolder.Name;
                }
                else if(localFolderMatch.Count() > 1)
                {

                }
                else if(localFolderMatch.Count() == 0)
                {
                    //no match is found? delete folder from box?
                }
            }
        }

        private List<EvoBoxFolder> Flatten(List<EvoBoxFolder> folderHierarchy )
        {
            //test it
            List<EvoBoxFolder> flattened = new List<EvoBoxFolder>();
            foreach (var folder in folderHierarchy)
            {
                flattened.AddRange(folder.Flatten(x => x.ChildFolders));
            }
            return flattened;
        }
        #endregion Map Local Folders to Box Folders on Box Folder IDs

        #region Read in All the Files in the folder
        public void ReadInFolderFiles(List<EvoBoxFolder> folders)
        {
            var flattened = Flatten(folders);
            foreach(var folder in flattened)
            {
                ReadFilesPerFolder(folder);
            }
        }
        private void ReadFilesPerFolder(EvoBoxFolder folder)
        {
            if(Directory.Exists(folder.FullPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folder.FullPath);
                foreach(var fileInfo in directoryInfo.GetFiles())
                {
                    folder.FileNames.Add(fileInfo.FullName);
                }
            }
        }
        #endregion

        #region Upload Files

        public void UploadAllFiles(List<EvoBoxFolder> folders)
        {
            var flattened =  Flatten(folders);
            foreach(var folder in flattened)
            {
                foreach( string file in folder.FileNames)
                {
                    EvoBoxService.ExecuteMainAsyncFileUpload(file, folder.BoxId, boxClient);
                }
                //
            }
        }

        #endregion

        #region Testing only
        private static void OnFindFolderComplete(BoxCollection<BoxItem> foldersCollection)
        {

        }

        private static void OnFolderCreatedComplete(BoxFolder f)
        {

        }
        #endregion Testing Only
    }



}
