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
            BoxFolderStructureManager boxFolderStructureManager = new BoxFolderStructureManager("testClient", "testJobId");
            string fullPath = Path.GetFullPath(@"LastSavedLocalFolderStructure.xml");
            boxFolderStructureManager.CreateLocalEvoBoxFolderStructure(fullPath);
        }
    }
}
