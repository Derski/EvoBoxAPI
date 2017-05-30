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

        public static async Task<BoxCollection<BoxItem>> FindFolderByName(string folderName)
        {
            try
            {
                var jsonPath = Path.Combine(AppContext.BaseDirectory, @"EvoBoxAPI\BoxConfig.json");
                Stream jsonFileStream = new FileStream(jsonPath, FileMode.Open);
                IBoxConfig boxConfig = Box.V2.Config.BoxConfig.CreateFromJsonFile(jsonFileStream);
                BoxJWTAuth boxJWT;
                boxJWT = new BoxJWTAuth(boxConfig);
                string adminToken = boxJWT.AdminToken();
                BoxClient adminClient = boxJWT.AdminClient(adminToken);
                var searchManager = adminClient.SearchManager;
                //var searchResults =  await searchManager.SearchAsync("name");
                var searchResults = await searchManager.SearchAsync("a");
                return searchResults;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<BoxFolder> CreateFolder(string rootFolderPath, string FolderName)
        {
            try
            {
                var jsonPath = Path.Combine(AppContext.BaseDirectory, @"EvoBoxAPI\BoxConfig.json");
                Stream jsonFileStream = new FileStream(jsonPath, FileMode.Open);
                IBoxConfig boxConfig = Box.V2.Config.BoxConfig.CreateFromJsonFile(jsonFileStream);
                BoxJWTAuth boxJWT;
                boxJWT = new BoxJWTAuth(boxConfig);
                string adminToken = boxJWT.AdminToken();
                BoxClient adminClient = boxJWT.AdminClient(adminToken);
                var folderManager = adminClient.FoldersManager;
                BoxFolderRequest r = new BoxFolderRequest();
                var parentFolderId = "0";//this should be root folder
                r.Description = "Test Description2";
                r.Name = "Test Name2";
                r.Type = BoxType.folder;
                BoxRequestEntity e = new BoxRequestEntity();
                e.Id = 0.ToString();
                r.Parent = e;

                var f = await  folderManager.CreateAsync(r);
                return f;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
              
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
            string CLIENT_ID = "el6xe0qqyixfm1rbx5pwwbobk8xlvldp";
            string CLIENT_SECRET = "tlCdTRvigNHvJgemByniJojcc9rMgEl5";
            string ENTERPRISE_ID = "20191180";
            string JWT_PRIVATE_KEY_PASSWORD = "2c62344d6622f91ee97cd8fc673dde48";
            string JWT_PUBLIC_KEY_ID = "wr52eig3";
            string JWT_PRIVATE_KEY = "-----BEGIN ENCRYPTED PRIVATE KEY-----\nMIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIVADZb1KhBXkCAggA\nMBQGCCqGSIb3DQMHBAjoqI0nZ91R/wSCBMhu3bDXtOAQNZt/BNz0p8BMlPHDy8Ej\nSokl+eSzPhxKeqg+VJf9Of+W94R55xyizbi7aCGAY1/2H3gXH667jHu+ogLjnvCq\n1DIDU2N88RdTu5I8TbIPUcY1vyqm8KS6NlTtLfbLcPO+XMdGuxmIeV9Ssek6T5WV\nnPcVr81O4drZrIJRTziobE9drkmnpIGHxCFUjF+UjyF+XyrNkSsh4FHAn9IpD3tY\n+6i7xKIi/ssetHWW1apgsxE418zTotsh6JGeZTGfwCEHLUz5Zzs1BGhUH5W1CJSC\ne9T2u39hCApCiWZ29qw8KpNwIUdpB01sdgR351fE+fZliIHMUoayus+eTUWSu3M0\nJtk8WTgszgGy4wUTzyFTliJo8/2pVPM2NILhR5/R60z1bxnQ4kNCrGyxkJ3BlJwT\ncVjkFoWdAE2Xf3vL1pjanIy9N1NzoPLz30cnvHy5VhZ2bYdkZ4bsWnwkb/liXorI\nLuMXbqbz3FJ/VXGOWbq+AiJWSSQ5q0grfaxcwr69QnefG7EA9rUz8AmG8zAG27La\nzr2sV8PjXhnSONDN1MCdcFGZkLx8rTC1KbJaTyLohD/+QdUMXGKVNscF6wy0iIjr\n03Bh1WkvuIVw5U9voIo/9rpV37i3QI7ogV4/uBUfJQxTQxIQmMfK97v1Afdred0M\nyern2WpnJ+LdlvB20emWcqua2nYnTPlAzmR1xPCDS6U3fEAmJTqtTRfoybrGC9eu\ngOMMg9NnWcUvrwPLYd1HbkPQLmSv429If4Jxxxp+Cp/CQ4kAAAdJBVj4YHcv0v/W\n/SP/gUv/+kvpo5gyVkUW/prvWuRUJh18niCJRp1i+KJd6Bg4iCtKImFk36JcU3vF\nTPBetN/LC4EkCwX1cSQbygcfZwCzK3x3SME7ZxnrtARSfBcu7341mfOTGdVx9VlH\nNyOmUso6BNPpwAHsE8eW9I9Mok4EYlTAlWMt/oe3bB8wDuh8+RDlhQMA28I4Mx5d\n89+FSQ/A1KTqby8O651ZHA02J1N8Q7Gwc4hLnfYlbrbnJTCvEAILS0AnufZYQ2Aq\naRlZRLQdTKOrJCZi6UM2RX8oev986iQa0KAj7IHd8BoweCrFYp0LgywlG4iAbYRC\nBLHb58LWR0Ups7xo5yTSualkMg/3dnL3Od2tP+Hs2IUAawl8r+HCz+EZkF3n+noc\nC1pGsevLsAJIpoOXPHieTUeWPALaaVmBkV76buM9Edws//evJyH3SpYYkOlT2xKu\nOi3/r5yhrPiXJRZqwV38zlw5dr9rihE8ilmig2Bb3lNYboIikNkvOwls/nZPj0RM\notxRbseQThS+V25iQWmKI39zzQne0wCE4hn5S9cOvP7GNazuungGgSdHGGvbF2X9\nRiahVlLhBo2MKQltXsgCrRPjCHhw7TlAYdc/DYwiReoiCuioK3EA3tKi7hMc1xfi\nX8pTMsKDgFHD6dDCvY3KA1pltYek1mLrNR0tM3vyHyMjEy7ERcDSmoxOoXkmSew6\nMdTcnCduTLGaS+Pb34Q6BNR72hx33fIaMB5ok/E10F3qfkthZm+kplFY5tvExvTF\ncRlkF5gHH//sIgv6rl54Vuy/cvq/NVxX0q6i5WbujAYh0OEAyEl0qdLvKIzrjP8N\n4vM=\n-----END ENCRYPTED PRIVATE KEY-----\n";

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


            ////test to create and delete a new app user
            //var userRequest = new BoxUserRequest() { Name = "test appuser", IsPlatformAccessOnly = true };
            //var appUser = await adminClient.UsersManager.CreateEnterpriseUserAsync(userRequest);
            //Console.WriteLine("Created App User");

            //var userToken = boxJWT.UserToken(appUser.Id);
            //var userClient = boxJWT.UserClient(userToken, appUser.Id);

            //var userDetails = await userClient.UsersManager.GetCurrentUserInformationAsync();
            //Console.WriteLine("\nApp User Details:");
            //Console.WriteLine("\tId: {0}", userDetails.Id);
            //Console.WriteLine("\tName: {0}", userDetails.Name);
            //Console.WriteLine("\tStatus: {0}", userDetails.Status);
            //Console.WriteLine();

            //await adminClient.UsersManager.DeleteEnterpriseUserAsync(appUser.Id, false, true);
            //Console.WriteLine("Deleted App User");
        }
    }

}
