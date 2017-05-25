using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EvoBoxAPI
{
    public static class BoxService
    {       
        public static async Task<BoxFile> ExecuteMainAsyncFileUpload()
        {
            var accessToken = "T0cdMnvK0hILFS2vbsvTHB1yY95TUISU";

            var fileName = "RemoteFileName";

            var localFilePath = "C:\\Users\\cderkowski\\Desktop\\Surface Display Software.pdf";

            var parentFolderId = "0";//this should be root folder

            //var timer = Stopwatch.StartNew();

            var refreshToken = "";
            var auth = new OAuthSession(accessToken, refreshToken, 3600, "bearer");

            var clientID = "0g6avu3n8udr3pfow3zlfpcu9i9e71tk";

            var config = new BoxConfig(clientID, clientID, new Uri("http://boxsdk"));
            var client = new BoxClient(config, auth);
            var file = File.OpenRead(localFilePath);
            var fileRequest = new BoxFileRequest
            {
                Name = fileName,
                Parent = new BoxFolderRequest { Id = parentFolderId }
            };

            var bFile = await client.FilesManager.UploadAsync(fileRequest, file);

            return bFile;
           
        }

        public static async Task JWTAuthAsync()
        {

            var boxConfig = new BoxConfig(CLIENT_ID, CLIENT_SECRET, ENTERPRISE_ID, JWT_PRIVATE_KEY, JWT_PRIVATE_KEY_PASSWORD, JWT_PUBLIC_KEY_ID);

            BoxJWTAuth boxJWT;
            try
            {
                boxJWT = new BoxJWTAuth(boxConfig);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            string adminToken = "";
            try
            {
                adminToken = boxJWT.AdminToken();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            Console.WriteLine("Admin Token: " + adminToken);
            Console.WriteLine();
            var adminClient = boxJWT.AdminClient(adminToken);

            Console.WriteLine("Admin root folder items");
            var items = await adminClient.FoldersManager.GetFolderItemsAsync("0", 500);
            items.Entries.ForEach(i =>
            {
                Console.WriteLine("\t{0}", i.Name);
                //if (i.Type == "file")
                //{
                //    var previewLink = adminClient.FilesManager.GetPreviewLinkAsync(i.Id).Result;
                //    Console.WriteLine("\tPreview Link: {0}", previewLink.ToString());
                //    Console.WriteLine();
                //}   
            });
            Console.WriteLine();

            var userRequest = new BoxUserRequest() { Name = "test appuser", IsPlatformAccessOnly = true };
            var appUser = await adminClient.UsersManager.CreateEnterpriseUserAsync(userRequest);
            Console.WriteLine("Created App User");

            var userToken = boxJWT.UserToken(appUser.Id);
            var userClient = boxJWT.UserClient(userToken, appUser.Id);

            var userDetails = await userClient.UsersManager.GetCurrentUserInformationAsync();
            Console.WriteLine("\nApp User Details:");
            Console.WriteLine("\tId: {0}", userDetails.Id);
            Console.WriteLine("\tName: {0}", userDetails.Name);
            Console.WriteLine("\tStatus: {0}", userDetails.Status);
            Console.WriteLine();

            await adminClient.UsersManager.DeleteEnterpriseUserAsync(appUser.Id, false, true);
            Console.WriteLine("Deleted App User");
        }
    }


}
