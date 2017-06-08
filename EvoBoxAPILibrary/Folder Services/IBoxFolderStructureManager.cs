using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvoBoxAPILibrary
{
    public interface IBoxFolderStructureManager
    {
        //string GetBoxClientJobIdPrefix { get; }

        //string GetBoxClientJobIdRootFolderName { get; }

        //void UpdateClient(string clientId);
        //void UpdateJobId(string jobId);

        EvoBoxFolder CreateLocalEvoBoxFolderStructure(TreeNodeCollection nodes, string clientId, string jobId);

        void ReadCloudFolderMetadataLocally(EvoBoxFolder evoBoxFolder, FolderManager folderManager, FileManager fileManager);
    }
}
