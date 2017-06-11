using EvoBoxAPILibrary.File_Services;
using System.Windows.Forms;

namespace EvoBoxAPILibrary
{
    public interface IBoxFolderStructureManager
    {

        EvoBoxFolder CreateLocalEvoBoxFolderStructureFromTreeNodes(TreeNodeCollection nodes, string clientId, string jobId);

        void ReadCloudFolderMetadataLocally(EvoBoxFolder evoBoxFolder, FolderManager folderManager, FileManager fileManager);

        void ReadInFolderFiles(EvoBoxFolder rootFolder);

        EvoBoxFolder TransformXMLtoBoxFolderStructure(string folderConfigFile, IClientJobInfo clientInfo);

        EvoBoxFolder TransformLocalBoxFolderStructureToCloudBoxFolderStructure
            (EvoBoxFolder localFolderStructure, IClientJobInfo clientInfo);
    }
}
