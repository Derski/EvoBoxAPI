using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Models;
using EvoBoxAPILibrary;
using System;
using System.IO;
using System.Threading.Tasks;

// this is a useful link:  https://github.com/box/box-windows-sdk-v2/tree/master/Box.V2/Managers

namespace EvoBoxAPILibrary
{
    public static class EvoBoxService
    {
        public static BoxClient GetAdminClient()
        {
            BoxClient adminClient = null;

            //var jsonPath = Path.Combine(AppContext.BaseDirectory, @"EvoBoxAPI\BoxConfig.json");
            //var jsonPath = @"C:\Users\cderkowski\Source\Repos\EvoBoxAPI\src\EvoBoxAPI\BoxConfig.json";
            //Stream jsonFileStream = new FileStream(jsonPath, FileMode.Open);
            //IBoxConfig boxConfig = Box.V2.Config.BoxConfig.CreateFromJsonFile(jsonFileStream);
            //IBoxConfig boxConfig = Box.V2.Config.BoxConfig.CreateFromJsonString();

            BoxConfig boxConfig = new BoxConfig
                (
                BoxConfigClass.clientID, 
                BoxConfigClass.clientSecret, 
                BoxConfigClass.enterpriseID, 
                BoxConfigClass.privateKey,
                BoxConfigClass.passphrase, 
                BoxConfigClass.publicKeyID
                );

            BoxJWTAuth boxJWT;
            boxJWT = new BoxJWTAuth(boxConfig);

            try
            {
                NtpLibrary.SystemTimeHack.CheckAndTryToFixSystemTime();
            }
            catch (Exception)
            {
                
            }
            

            try
            {
                string adminToken = boxJWT.AdminToken();
                adminClient = boxJWT.AdminClient(adminToken);
                adminClient.Auth.SessionAuthenticated += Auth_SessionAuthenticated;
            }
            catch (Exception )
            {
                    throw;
            }
            return adminClient;
        }

        private static void Auth_SessionAuthenticated(object sender, SessionAuthenticatedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static async Task<BoxCollection<BoxItem>> FindFoldersByKeyword(string keyword, BoxClient client)
        {
            try
            {
                BoxClient adminClient = GetAdminClient();
                var searchManager = adminClient.SearchManager;

                //var searchResults = await searchManager.SearchAsync(keyword);
                //return searchResults;

                var searchResults = searchManager.SearchAsync(keyword);
                return searchResults.Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<BoxCollection<BoxItem>> GetFolderItemsById(string id, BoxClient client)
        {
            try
            {
                BoxClient adminClient = GetAdminClient();
                var folderManager = adminClient.FoldersManager;

                //todo read up more about async methods in c#
                //var searchResults = await searchManager.SearchAsync(keyword);
                //return searchResults;

                var searchResults = folderManager.GetFolderItemsAsync(id, limit: 1000);

                return searchResults.Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<BoxFolder> CreateFolder(string FolderName, string parentId, BoxClient client)
        {
            try
            {
                var folderManager = client.FoldersManager;
                BoxFolderRequest r = new BoxFolderRequest();
                r.Description = FolderName;
                r.Name = FolderName;
                r.Type = BoxType.folder;
                BoxRequestEntity e = new BoxRequestEntity();
                e.Id = parentId;
                r.Parent = e;
                //var f = await  folderManager.CreateAsync(r);
                var f =  folderManager.CreateAsync(r);
                return f.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BoxFile> ExecuteMainAsyncFileUpload(string localFilePath,string parentFolderId, BoxClient client)
        {
            var filestream = File.OpenRead(localFilePath);
            string fileName =  Path.GetFileName(localFilePath);

            var fileRequest = new BoxFileRequest
            {
                Name = fileName,
                Parent = new BoxFolderRequest { Id = parentFolderId },
            };
            try
            {
                var bFile = await client.FilesManager.UploadAsync(fileRequest, filestream);
                return bFile;
            }
            catch (Box.V2.Exceptions.BoxPreflightCheckConflictException<Box.V2.Models.BoxFile> ex)
            {
                BoxFile conflictingItem = ex.ConflictingItem;
                var sha1 =  conflictingItem.Sha1;
                var fileId = conflictingItem.Id;
                var conflictingFileName = conflictingItem.Name;
                var type = conflictingItem.Type;

                throw;
            }
            
           
        }

    }

}
