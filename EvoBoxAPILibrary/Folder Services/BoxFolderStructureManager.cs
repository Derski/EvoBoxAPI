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

namespace EvoBoxAPILibrary
{
    public class BoxFolderStructureManager: IBoxFolderStructureManager
    {
        string _jobId;
        string _clientId;

        public BoxFolderStructureManager(string clientId, string jobId)
        {
            _jobId = jobId;
            _clientId = clientId;
        }

        public void UpdateClient(string clientId)
        {
            _clientId = clientId;
        }
        public void UpdateJobId(string jobId)
        {
            _jobId = jobId;
        }

        public string  GetBoxClientJobIdPrefix
        {
            get
            {
                return _clientId + "_" + _jobId + "_";
            }
        }
        public string GetBoxClientJobIdRootFolderName
        {
            get
            {
                return _clientId + "_" + _jobId;
            }
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
                    var firstBoxBode = node.Nodes[0];
                    var tag = (TreeNodeCustomData)firstBoxBode.Tag;
                    EvoBoxFolder folder = new EvoBoxFolder(firstBoxBode.FullPath, firstBoxBode.Text, firstBoxBode.Checked,tag.FileFilter);
                    evoBoxFolders.Add(folder);
                    AddChildFolders(folder, firstBoxBode.Nodes);
                }
            }
            //create jobId Folder and Client Folder
            string jobIdPrefix = GetBoxClientJobIdRootFolderName;

            EvoBoxFolder ClientJobPrefixFolder = new EvoBoxFolder(jobIdPrefix);
            ClientJobPrefixFolder.ChildFolders.AddRange(evoBoxFolders);

            EvoBoxFolder ClientRootFolder = new EvoBoxFolder(clientId);
            ClientRootFolder.ChildFolders.Add(ClientJobPrefixFolder);

            ReadInFolderFiles(ClientRootFolder);

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
            return DeserializeTreeView(new TreeView(), folderStructureFilePath);
        }

        //*************************test
        // Xml tag for node, e.g. 'node' in case of <node></node>
        private const string XmlNodeTag = "node";
        // Xml attributes for node e.g. <node text="Asia" tag="" 
        // imageindex="1"></node>
        private const string XmlNodeTextAtt = "text";
        private const string XmlNodeCheckedAtt = "checked";
        private const string XmlNodeExpandedAtt = "expanded";
        private const string XmlNodeTagAtt = "tag";
        private const string XmlNodeImageIndexAtt = "imageindex";
        private EvoBoxFolder DeserializeTreeView(TreeView treeView, string fileName )
        {
            EvoBoxFolder rootFolder = null;
            XmlTextReader reader = null;
            try
            {
                // disabling re-drawing of treeview till all nodes are added
                treeView.BeginUpdate();
                reader = new XmlTextReader(fileName);
                System.Windows.Forms.TreeNode parentNode = null;
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
                                for (int i = 0; i < attributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    SetAttributeValue(newNode,
                                                 reader.Name, reader.Value);
                                    attributesDict.Add(reader.Name, reader.Value);
                                }
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
                            // add new node to Parent Node or TreeView
                            if (parentNode != null)
                                parentNode.Nodes.Add(newNode);
                            else
                                treeView.Nodes.Add(newNode);

                            //same as above for the EvoBoxFolder
                            if(rootFolder != null )
                            {
                                if(newFolder != null)
                                {
                                    rootFolder.ChildFolders.Add(newFolder);
                                }
                                
                            }
                            else
                            {

                            }

                            // making current node 'ParentNode' if its not empty
                            if (!isEmptyElement)
                            {
                                parentNode = newNode;
                            }
                        }
                    }
                    // moving up to in TreeView if end tag is encountered
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            parentNode = parentNode.Parent;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.XmlDeclaration)
                    {
                        //Ignore Xml Declaration                    
                    }
                    else if (reader.NodeType == XmlNodeType.None)
                    {
                        return rootFolder;
                    }
                    else if (reader.NodeType == XmlNodeType.Text)
                    {
                        parentNode.Nodes.Add(reader.Value);
                    }

                }
            }
            finally
            {
                // enabling redrawing of treeview after all nodes are added
                treeView.EndUpdate();
                reader.Close();
                treeView.ExpandAll();
            }
            return rootFolder;
        }
        /// <span class="code-SummaryComment"><summary></span>
        /// Used by Deserialize method for setting properties of
        /// TreeNode from xml node attributes
        /// <span class="code-SummaryComment"></summary></span>
        private void SetAttributeValue(System.Windows.Forms.TreeNode node,
                           string propertyName, string value)
        {
            if (propertyName == XmlNodeTextAtt)
            {
                node.Text = value;
            }
            else if (propertyName == XmlNodeImageIndexAtt)
            {
                node.ImageIndex = int.Parse(value);
            }
            else if (propertyName == XmlNodeTagAtt)
            {
                node.Tag = new TreeNodeCustomData(value);
            }
            else if (propertyName == XmlNodeCheckedAtt)
            {
                bool isChecked = false;
                bool.TryParse(value, out isChecked);
                node.Checked = isChecked;
            }
            else if (propertyName == XmlNodeExpandedAtt)
            {
                bool isExpanded = false;
                bool.TryParse(value, out isExpanded);
                if (isExpanded)
                {
                    node.Expand();
                }

            }
        }
        //*********************
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
