using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvoBoxAPILibrary;
using Box.V2;
using System.Collections.Generic;

namespace EvoBoxAPILibrary.Tests
{
    //depends on file structure setup on a box folder
    [TestClass]
    public class FileManagerTests
    {
        [TestMethod]
        public void GetBoxFileInfoForBoxFolders_GivenFolderAndFileReturnsFile()
        {
            //arrange
            BoxClient client =  EvoBoxService.GetAdminClient();
            FileManager manager = new FileManager(client);
            //TODO the box testing will need a very extensive setup to mimic real life use cases better
            string boxFolderId = "29006365354";

            List<EvoBoxFile> files = new List<EvoBoxFile>();
            var localName = @"C:\RawData\rawData20170426103738.csv";
            EvoBoxFile localFile = new EvoBoxFile(localName);
            files.Add(localFile);
            //should upload the file and after upload verify that they are equal
            manager.GetBoxFileInfoForBoxFolders(boxFolderId, files);

            //act
            var boxFile =  files[0];

            //assert
            Assert.IsTrue(boxFile.MostRecentAlreadyUploaded);
        }
    }
}
