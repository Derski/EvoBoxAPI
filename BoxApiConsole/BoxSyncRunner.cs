
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
            string folderConfigFile = XMLFolderConfigurationFileProvider.FolderStructureFileFullPath;
            TreeNodeXMLSerializer treeNodeXMLSerializer = new TreeNodeXMLSerializer();
            IClientJobInfo clientInfo = new ClientJobInfoStub();
            EvoBoxFolder folder =  treeNodeXMLSerializer.TransformXMLtoBoxFolderStructure(folderConfigFile,clientInfo);

        }

    }
}
