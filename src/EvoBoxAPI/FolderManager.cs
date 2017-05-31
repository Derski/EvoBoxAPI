using Box.V2;
using Box.V2.Models;
using Box.V2.Services;
using EvoBoxAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace BoxConsole
{

    public class FolderManager
    {
        public string ClientId;
        public string JobId;
        public string ClientJobIdPrefix
        {
            get
            {
                return ClientId + "_" + JobId + "_";
            }
        }
        BoxClient boxClient;

        #region Constructor
        //Constructor
        public FolderManager(string clientId="Phoenix",string jobId="test123")
        {
            ClientId = clientId;
            JobId = jobId;
            boxClient = EvoBoxAPI.EvoBoxService.GetAdminClient();
        }
        #endregion Constructor

        #region Folder Structure Create
        public List<EvoBoxFolder> CreateNewBoxFolderStructure(List<EvoBoxFolder> localFolders)
        {
            BoxFolder clientRootFolder = BoxFolderCreate(ClientId, "0", boxClient);
            if(clientRootFolder != null)
            {
                BoxFolder jobRootFolder = BoxFolderCreate(ClientId + "_" + JobId, clientRootFolder.Id, boxClient);
                if(jobRootFolder != null)
                {
                    foreach(var folder in localFolders)
                    {
                        CreateFolderHierarchy(folder, jobRootFolder.Id);
                    }
                }
            }

            return null;
        }

        private void CreateFolderHierarchy(EvoBoxFolder currentFolder,string parentFolderId)
        {
            BoxFolder currentBoxFolder = 
            BoxFolderCreate(ClientId + "_" + JobId + "_" + currentFolder.FolderName, parentFolderId, boxClient);
            if(currentBoxFolder != null)
            {
                currentFolder.BoxId = currentBoxFolder.Id;
                currentFolder.BoxParentId = parentFolderId;
                foreach(var childFolder in currentFolder.ChildFolders)
                {
                    CreateFolderHierarchy(childFolder, currentBoxFolder.Id);
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

        #region Map Local Folders to Box Folders on Box Folder IDs
        public void GetBoxFolderIdsForFileFolders(List<EvoBoxFolder> localFolders)
        {
            Task<BoxCollection<BoxItem>> task = EvoBoxService.FindFoldersByKeyword(ClientId,boxClient);
            var awaiter = task.GetAwaiter();
            //awaiter.OnCompleted(() => OnFindFolderComplete(awaiter.GetResult()));
            var flattened = Flatten(localFolders);
            foreach (BoxItem boxFolder in awaiter.GetResult().Entries)
            {
                string fileName =  boxFolder.Name.Replace(ClientJobIdPrefix, "");
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

    //https://stackoverflow.com/questions/18165460/how-to-search-hierarchical-data-with-linq
    public static class Linq
    {
        public static IEnumerable<T> Flatten<T>(this T source, Func<T, IEnumerable<T>> selector)
        {
            return selector(source).SelectMany(c => Flatten(c, selector))
                                   .Concat(new[] { source });
        }
    }

}
