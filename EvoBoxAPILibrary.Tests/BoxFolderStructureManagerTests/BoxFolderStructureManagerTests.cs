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
       // [DeploymentItem("LastSavedLocalFolderStructure.xml", "BoxFolderStructureManagerTests")]
        [DeploymentItem("BoxFolderStructureManagerTests\\LastSavedLocalFolderStructure.xml")]
        public void TestCreateLocalEvoBoxFolderStructure_ShouldCreateEvoBoxFolderHierarchy()
        {
            IClientJobInfo clientJobInfo = new ClientJobInfoStub();
            clientJobInfo.CurrentSelectedClient = "testClient";
            clientJobInfo.CurrentSelectedJobId = "testJobId";
            //arrange
            BoxFolderStructureManager boxFolderStructureManager = new BoxFolderStructureManager(clientJobInfo);
            string fullPath = Path.GetFullPath(@"LastSavedLocalFolderStructure.xml");
            var folderStructure = boxFolderStructureManager.CreateLocalEvoBoxFolderStructure(fullPath);
            //act

            //assert

        }
    }
}
