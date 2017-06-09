using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EvoBoxAPILibrary.File_Services;

namespace EvoBoxAPILibrary
{
    public interface IBoxFolderStructureManager
    {

        EvoBoxFolder CreateLocalEvoBoxFolderStructure(TreeNodeCollection nodes, string clientId, string jobId);

        void ReadCloudFolderMetadataLocally(EvoBoxFolder evoBoxFolder, FolderManager folderManager, FileManager fileManager);
    }
}
