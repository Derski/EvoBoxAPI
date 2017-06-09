using Box.V2;
using Box.V2.Models;
using Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using EvoBoxAPILibrary.File_Services;

namespace EvoBoxAPILibrary
{
    public class BoxFolderStructureManager: IBoxFolderStructureManager
    {
        IClientJobInfo _clientJobInfo;
        public BoxFolderStructureManager(IClientJobInfo clientJobInfo)
        {
            _clientJobInfo = clientJobInfo;
        }
        
        #region Create box folder structure from client, job and local folders  
        public EvoBoxFolder CreateLocalEvoBoxFolderStructure
            (TreeNodeCollection nodes,string clientId, string jobId)
        {
            List<EvoBoxFolder> evoBoxFolders = new List<EvoBoxFolder>();

            foreach (TreeNode node in nodes)
            {
                if(node.Checked)
                {
                    //skip the top node, for ex. we only want the job work folder, not the desktop folder
                    var firstBoxNode = node.Nodes[0];
                    var tag = (TreeNodeCustomData)firstBoxNode.Tag;
                    EvoBoxFolder folder = new EvoBoxFolder(firstBoxNode.FullPath, firstBoxNode.Text, firstBoxNode.Checked,tag.FileFilter);
                    evoBoxFolders.Add(folder);
                    AddChildFolders(folder, firstBoxNode.Nodes);
                }
            }

            EvoBoxFolder ClientRootFolder = null;

            if (evoBoxFolders.Count>0)
            {
                //create jobId Folder and Client Folder
                string jobIdPrefix = _clientJobInfo.GetBoxClientJobIdRootFolderName;

                EvoBoxFolder ClientJobPrefixFolder = new EvoBoxFolder(jobIdPrefix);
                ClientJobPrefixFolder.ChildFolders.AddRange(evoBoxFolders);

                ClientRootFolder = new EvoBoxFolder(clientId);
                ClientRootFolder.ChildFolders.Add(ClientJobPrefixFolder);

                ReadInFolderFiles(ClientRootFolder);
            }

            return ClientRootFolder;
        }
        private static void AddChildFolders(EvoBoxFolder parentFolder, TreeNodeCollection nodes)
        {
            foreach(TreeNode childNode in nodes)
            {
                if(childNode.Checked)
                {
                    var tag =  (TreeNodeCustomData)childNode.Tag;
                    EvoBoxFolder childFolder =
                    new EvoBoxFolder(childNode.FullPath, childNode.Text, childNode.Checked,tag.FileFilter);
                    parentFolder.ChildFolders.Add(childFolder);
                    AddChildFolders(childFolder, childNode.Nodes);
                }
            }
            
        }


        public EvoBoxFolder CreateLocalEvoBoxFolderStructure(string folderStructureFilePath)
        {
            return DeserializeTreeView(folderStructureFilePath);
        }
        
        // Xml tag for node, e.g. 'node' in case of <node></node>
        private const string XmlNodeTag = "node";
        // Xml attributes for node e.g. <node text="Asia" tag="" 
        // imageindex="1"></node>
        private const string XmlNodeTextAtt = "text";
        private const string XmlNodeCheckedAtt = "checked";
        private const string XmlNodeExpandedAtt = "expanded";
        private const string XmlNodeTagAtt = "tag";
        private const string XmlNodeImageIndexAtt = "imageindex";

        private EvoBoxFolder DeserializeTreeView(string fileName)
        {
            EvoBoxFolder parentFolder = new EvoBoxFolder("root");
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(fileName);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            System.Windows.Forms.TreeNode newNode = new System.Windows.Forms.TreeNode();
                            EvoBoxFolder newFolder = null;
                            bool isEmptyElement = reader.IsEmptyElement;

                            // loading node attributes
                            int attributeCount = reader.AttributeCount;
                            if (attributeCount > 0)
                            {
                                Dictionary<string, string> attributesDict = new Dictionary<string, string>();
                                
                                if(attributesDict.ContainsKey(XmlNodeCheckedAtt) && attributesDict[XmlNodeCheckedAtt]== "True")
                                {
                                    newFolder = new EvoBoxFolder(attributesDict[XmlNodeTextAtt]);
                                    if(attributesDict.ContainsKey(XmlNodeTagAtt))
                                    {
                                        var tagInfo = new TreeNodeCustomData(attributesDict[XmlNodeTagAtt]);
                                        newFolder.FileFilter = tagInfo.FileFilter;
                                        newFolder.FullPath = tagInfo.FullFilePath;
                                    }
                                }
                            }
                            //same as above for the EvoBoxFolder
                            if(parentFolder != null )
                            {
                                if(newFolder != null)
                                {
                                    parentFolder.ChildFolders.Add(newFolder);
                                    newFolder.Parent = parentFolder;
                                }
                            }
                            // depth first search
                            if (!isEmptyElement)
                            {
                                parentFolder = newFolder;
                            }
                        }
                    }
                    // moving up to in TreeView if end tag is encountered
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            parentFolder = parentFolder.Parent;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.XmlDeclaration)
                    {
                        //Ignore Xml Declaration                    
                    }
                    else if (reader.NodeType == XmlNodeType.None)
                    {
                        return parentFolder;
                    }
                    else if (reader.NodeType == XmlNodeType.Text)
                    {
                        //not yet sure where to store client and job id data...
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            return parentFolder;
        }

        #endregion

        #region Validate with Box which local folders or files already exist in the cloud and populate the box IDs and SHA1 hash values
        /// <summary>
        /// Take a local folder structure, connect to the box server and update all box attributes for the given folders and files
        /// This should populate all the box IDs and the sha1 hash to perform a checksum
        /// </summary>
        /// <param name="evoBoxFolder"> The representation of the local folder structure to be synchronized to Box 
        /// the root folder always begins with the Client Name and the immidiate subfolder will be the Job ID
        /// </param>
        public void ReadCloudFolderMetadataLocally
            (EvoBoxFolder evoBoxFolder, FolderManager folderManager, FileManager fileManager)
        {
            //for all folders populate the folder Box IDs
            folderManager.FindFromClientRootAndPopulateBoxAttributes(evoBoxFolder);
            var flat = evoBoxFolder.Flatten(x => x.ChildFolders);
            foreach(EvoBoxFolder folder in evoBoxFolder.Flatten(x => x.ChildFolders).Where(f=>f.BoxId != null))
            {
                fileManager.GetBoxFileInfoForBoxFolders(folder);
            }
            
        }
        #endregion

        #region Read in All the Files in the folder
        private void ReadInFolderFiles(EvoBoxFolder rootFolder)
        {
            foreach (var folder in rootFolder.Flatten(x => x.ChildFolders))
            {
                ReadFilesPerFolder(folder);
            }
        }
        private void ReadFilesPerFolder(EvoBoxFolder folder)
        {
            if (Directory.Exists(folder.FullPath))
            {
                //FILTER
                var fileExtentions = folder.FileFilter.Replace("*.", ".").Split('|').ToList();
                DirectoryInfo directoryInfo = new DirectoryInfo(folder.FullPath);
                foreach (var fileInfo in directoryInfo.GetFiles())
                {
                    var extension = fileInfo.Extension;
                    if (fileExtentions.Contains(extension) || fileExtentions.Contains(".*"))
                    {
                        folder.FileNames.Add(new EvoBoxFile(fileInfo.FullName));
                    }
                }
            }
        }
        #endregion
    }

}
