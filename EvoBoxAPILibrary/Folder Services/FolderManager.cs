using Box.V2;
using Box.V2.Models;
using Box.V2.Services;
using EvoBoxAPILibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Extensions;
using EvoBoxAPILibrary;

namespace EvoBoxAPILibrary
{
    public class FolderManager:IFolderManager
    {
        public string AdminToken
        {
            get
            {
                if(_boxClient==null)
                {
                    return "Not Currently Authenticaten";
                }
                else
                {
                    try
                    {
                        return _boxClient.Auth.Session.AccessToken;
                    }
                    catch(Exception ex)
                    {
                        return ex.Message;
                    }
                }
                
            }
        }

        private BoxClient _boxClient;
        private BoxFolderStructureManager _boxFolderStructureManager;
        private IClientJobInfo _clientJobInfo;
        #region Constructor
        //Constructor
        public FolderManager(BoxClient boxClient, BoxFolderStructureManager boxFolderStructureManager, IClientJobInfo clientJobInfo)
        {
            _boxClient = boxClient;
            _boxFolderStructureManager = boxFolderStructureManager;
            _clientJobInfo = clientJobInfo;
        }
        #endregion Constructor

        #region Folder Structure Create
        public void CreateNewBoxFolderStructure
            (EvoBoxFolder localFolder, string clientId, string jobId)
        {
            var clientJobIdFolder = _clientJobInfo.GetBoxClientJobIdRootFolderName;
            var clientJobIdPrefix = _clientJobInfo.GetBoxClientJobIdPrefix;

            if (localFolder.BoxId == null || localFolder.BoxId=="0")
            {
                BoxFolder clientRootFolder = BoxFolderCreate(clientId, "0", _boxClient);
                if (clientRootFolder != null)
                {
                    localFolder.BoxId = clientRootFolder.Id;
                    localFolder.BoxParentId = "0";
                    var jobIdFolder =  localFolder.ChildFolders[0];
                    BoxFolder jobRootFolder = BoxFolderCreate(clientJobIdFolder, clientRootFolder.Id, _boxClient);
                    if (jobRootFolder != null)
                    {
                        jobIdFolder.BoxId = jobRootFolder.Id;
                        jobIdFolder.BoxParentId = clientRootFolder.Id;
                        CreateFolderHierarchy(jobIdFolder, jobRootFolder.Id, clientJobIdPrefix);
                    }
                }
            }
            else
            {
                var jobIdFolder = localFolder.ChildFolders[0];
                //root already exists, see if job if exists
                if (jobIdFolder.BoxId == null)
                {
                    BoxFolder jobRootFolder = BoxFolderCreate(clientJobIdFolder, localFolder.BoxId, _boxClient);
                    if (jobRootFolder != null)
                    {
                        jobIdFolder.BoxId = jobRootFolder.Id;
                        jobIdFolder.BoxParentId = jobRootFolder.Parent.Id;
                        CreateFolderHierarchy(jobIdFolder, jobRootFolder.Id, clientJobIdPrefix);
                    }
                }
                else
                {
                    //Client Job Id Folder already exists 
                    CreateFolderHierarchy(jobIdFolder, jobIdFolder.BoxId, clientJobIdPrefix);
                }
            }
        }

        private void CreateFolderHierarchy
            (EvoBoxFolder currentFolder,
            string parentFolderId,
            string clientJobIdPrefix)
        {
            if(currentFolder.BoxId == null)
            {
                BoxFolder currentBoxFolder =
                    BoxFolderCreate(clientJobIdPrefix + currentFolder.FolderName, parentFolderId, _boxClient);
                if (currentBoxFolder != null)
                {
                    currentFolder.BoxId = currentBoxFolder.Id;
                    currentFolder.BoxParentId = parentFolderId;
                    foreach (var childFolder in currentFolder.ChildFolders)
                    {
                        CreateFolderHierarchy(childFolder, currentBoxFolder.Id, clientJobIdPrefix);
                    }
                }
            }
            else
            {
                foreach (var childFolder in currentFolder.ChildFolders)
                {
                    CreateFolderHierarchy(childFolder, currentFolder.BoxId, clientJobIdPrefix);
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
            Task<BoxCollection<BoxItem>> task = EvoBoxService.FindFoldersByKeyword(clientId, _boxClient);
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
            Task<BoxCollection<BoxItem>> task = EvoBoxService.FindFoldersByKeyword(clientJobPrefix, _boxClient);
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

        /// <summary>
        /// LocalFolders start with Client and JobId Prefix
        /// </summary>
        /// <param name="localFolders"></param>
        public void FindFromClientRootAndPopulateBoxAttributes(EvoBoxFolder localFolder)
        {
            var ClientRootFolder = localFolder;
            BoxItem boxClientRoot =  FindRootClientFolder(ClientRootFolder.FolderName);           

            if(boxClientRoot != null)
            {
                localFolder.BoxId = boxClientRoot.Id;
                localFolder.BoxParentId = "0";

                var items = EvoBoxService.GetAdminClient().FoldersManager.GetFolderItemsAsync(boxClientRoot.Id,10).Result;
                var ClientJobIdRootFolder = ClientRootFolder.ChildFolders[0];
                BoxItem boxClientJobIdRoot = 
                items.Entries.SingleOrDefault(i => i.Name == ClientJobIdRootFolder.FolderName);
                if(boxClientJobIdRoot != null)
                {
                    ClientJobIdRootFolder.BoxId = boxClientJobIdRoot.Id;
                    ClientJobIdRootFolder.BoxParentId = boxClientRoot.Id;
                    loopthroughkids(boxClientJobIdRoot, ClientJobIdRootFolder, ClientJobIdRootFolder.FolderName+"_");
                }
            }
        }
       
        private void loopthroughkids(BoxItem boxitem, EvoBoxFolder localFolder, string clientJobPrefix)
        {
            var boxitems = EvoBoxService.GetAdminClient().FoldersManager.GetFolderItemsAsync(boxitem.Id,200);
            var localfolders = localFolder.ChildFolders;
            foreach(var childboxitem in boxitems.Result.Entries)
            {
                var match = 
                localfolders.SingleOrDefault
                (l => l.FolderName == childboxitem.Name.Replace(clientJobPrefix,""));
                if(match != null)
                {
                    match.BoxFolderName = childboxitem.Name;
                    match.BoxId = childboxitem.Id;
                    match.BoxParentId = boxitem.Id;
                    loopthroughkids(childboxitem, match, clientJobPrefix);
                }
            }
        }

        #endregion

        #region Upload Files

        public void UploadAllFiles(EvoBoxFolder rootFolder)
        {
            foreach(var folder in rootFolder.Flatten(x => x.ChildFolders))
            {
                foreach( EvoBoxFile file in folder.FileNames)
                {
                    if(!file.MostRecentAlreadyUploaded)
                    {
                        var task = EvoBoxService.ExecuteMainAsyncFileUpload(file.FullLocalName, folder.BoxId, _boxClient);
                        var awaiter = task.GetAwaiter();
                        try
                        {
                            awaiter.OnCompleted(() => OnFileUploadAsyncComplete(awaiter.GetResult()));
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        #endregion

        #region Returning from Async Events
        private static void OnFileUploadAsyncComplete(BoxFile file)
        {

        }
        private static void OnFindFolderComplete(BoxCollection<BoxItem> foldersCollection)
        {

        }

        private static void OnFolderCreatedComplete(BoxFolder f)
        {

        }
        #endregion Returning from Async Events
    }

}
