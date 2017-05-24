using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
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
            var accessToken = "hL80NM47Rv2OKCpgbJg7jzwe0DRck3ME";

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
    }


}
