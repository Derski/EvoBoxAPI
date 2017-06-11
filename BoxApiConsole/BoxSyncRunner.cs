
using EvoBoxAPILibrary;
using EvoBoxAPILibrary.File_Services;

namespace BoxApiConsole
{
    public class BoxSyncRunner
    {
        public BoxSyncRunner()
        {
            
        }

        public void RunSync()
        {
            //compose the whole damn thing starting from xml file

            string folderConfigFile = XMLFolderConfigurationFileProvider.FolderStructureFileFullPath;
            TreeNodeXMLSerializer treeNodeXMLSerializer = new TreeNodeXMLSerializer();
            IClientJobInfo clientInfo = new ClientJobInfoStub();
            BoxFolderStructureManager folderStructureManager = new BoxFolderStructureManager(clientInfo);
            EvoBoxFolder rootClientFolder = folderStructureManager.TransformXMLtoBoxFolderStructure(folderConfigFile,clientInfo);
            folderStructureManager.VerifyLocalFolderStructureFromBoxFolder(rootClientFolder);
            folderStructureManager.ReadInFolderFiles(rootClientFolder);
            

        }

    }
}
