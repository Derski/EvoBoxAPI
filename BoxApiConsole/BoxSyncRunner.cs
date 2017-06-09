using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EvoBoxAPILibrary.File_Services;
using EvoBoxAPILibrary;

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
            EvoBoxFolder folder =  treeNodeXMLSerializer.TransformXMLtoBoxFolderStructure(folderConfigFile);

        }

    }
}
