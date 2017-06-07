using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoBoxAPILibrary
{
    public interface IFolderManager
    {
        string AdminToken { get; }

        void CreateNewBoxFolderStructure(EvoBoxFolder localFolder, string clientId, string jobId);

        BoxItem FindRootClientFolder(string clientId);

        BoxItem FindRootJobIdFolder(string parentId, string clientJobPrefix);

        void FindFromClientRootAndPopulateBoxAttributes(EvoBoxFolder localFolder);

        void UploadAllFiles(EvoBoxFolder rootFolder);
    }
}
