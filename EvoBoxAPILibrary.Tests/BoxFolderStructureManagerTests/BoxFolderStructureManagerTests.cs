using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvoBoxAPILibrary;
using System.IO;
using System.Reflection;

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
            BoxFolderStructureManager boxFolderStructureManager = new BoxFolderStructureManager(clientJobInfo);
            string fullPath = Path.GetFullPath(@"FolderStructureConfiguration.xml");
            var folderStructure = boxFolderStructureManager.CreateLocalEvoBoxFolderStructure(fullPath);
            //act

            //assert

        }
    }
}
