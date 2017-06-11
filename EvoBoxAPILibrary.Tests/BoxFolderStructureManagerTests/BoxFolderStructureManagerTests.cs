using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EvoBoxAPILibrary.Tests
{
    [TestClass]
    public class BoxFolderStructureManagerTests
    {
        [TestMethod]
        [DeploymentItem("BoxFolderStructureManagerTests\\FolderStructureConfiguration.xml")]
        public void TestCreateLocalEvoBoxFolderStructure_ShouldCreateEvoBoxFolderHierarchy()
        {
            IClientJobInfo clientJobInfo = new ClientJobInfoStub();
            clientJobInfo.CurrentSelectedClient = "testClient";
            clientJobInfo.CurrentSelectedJobId = "testJobId";
            //arrange
            BoxFolderStructureManager folderStructureManager = new BoxFolderStructureManager(clientJobInfo);
            string fullPath = Path.GetFullPath(@"FolderStructureConfiguration.xml");
            //act
            folderStructureManager.TransformXMLtoBoxFolderStructure(fullPath, clientJobInfo);
            //assert

        }
    }
}
