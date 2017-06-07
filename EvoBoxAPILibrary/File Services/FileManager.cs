using Box.V2;
using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoBoxAPILibrary
{
    public class FileManager : IFileManager
    {
        BoxClient _client;
        public FileManager(BoxClient client)
        {
            _client = client;
        }

        public void GetBoxFileInfoForBoxFolders(EvoBoxFolder folder)
        {
            GetBoxFileInfoForBoxFolders(folder.BoxId, folder.FileNames);
        }
        public void GetBoxFileInfoForBoxFolders(string evoBoxFolderId, List<EvoBoxFile> boxFiles)
        {
            var task = EvoBoxService.GetFolderItemsById(evoBoxFolderId, _client);
            var awaiter = task.GetAwaiter();
            foreach (BoxItem boxItem in awaiter.GetResult().Entries)
            {
                if (boxItem is BoxFile)
                {
                    EvoBoxFile evoBoxFile = 
                    boxFiles.FirstOrDefault(f=>f.LocalFileName== boxItem.Name);
                    if(evoBoxFile != null)
                    {
                        evoBoxFile.EvoBoxFileId = boxItem.Id;
                        evoBoxFile.SHA1_Box = ((BoxFile)boxItem).Sha1;
                    }
                }  
            }
        }
    }
}
