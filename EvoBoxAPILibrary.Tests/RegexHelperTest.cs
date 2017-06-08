using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvoBoxAPILibrary;
namespace EvoBoxAPILibrary.Tests
{
    [TestClass]
    public class RegexHelperTest
    {
        [TestMethod]
        public void ExtractClientAndJobIds_PassingXMLSerializerStartElement_ShouldExtractClientandJobId()
        {
            //arrange
            string clientId = "Phoenix";
            string jobId = "Test123";

            string xmlStartElement = "TreeView Local Folder Structure ";
            xmlStartElement += "ClientId=" + clientId + "; ";
            xmlStartElement += "JobId=" + jobId;

            clientId = jobId = "";
            //act
            RegexHelper.ExtractClientAndJobIds(xmlStartElement, out clientId, out jobId);

            //assert
            Assert.AreEqual(clientId, "Phoenix");
            Assert.AreEqual(jobId, "Test123");
        }
    }
}
