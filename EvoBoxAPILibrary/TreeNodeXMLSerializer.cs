using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace EvoBoxAPILibrary
{
    public class TreeNodeXMLSerializer
    {
        #region Save File Section to XML
        public void SaveCurrentSelection(string fileName, TreeNodeCollection nodes, string clientId, string jobId)
        {
            if (File.Exists(fileName))
            {
                string xmlClientInfoElement = "ClientId=" + clientId+"; ";
                xmlClientInfoElement += "JobId=" + jobId;

                XmlTextWriter textWriter = new XmlTextWriter(fileName, System.Text.Encoding.ASCII);
                // writing the xml declaration tag
                textWriter.WriteStartDocument();
                //textWriter.WriteRaw("\r\n");
                // writing the main tag that encloses all node tags
                textWriter.WriteStartElement(XmlJobFoldersTag);
                textWriter.WriteAttributeString(XmlJobFoldersClientInfoAtt, xmlClientInfoElement);

                // save the nodes, recursive method
                SaveNodes(nodes, textWriter);

                textWriter.WriteEndElement();

                textWriter.Close();
            }


        }


        // Xml tag for node, e.g. 'node' in case of <node></node>
        private const string XmlNodeTag = "node";
        private const string XmlJobFoldersTag = "jobfolders";
        private const string XmlJobFoldersClientInfoAtt = "clientjobinfo";
        // Xml attributes for node e.g. <node text="Asia" tag="" 
        // imageindex="1"></node>
        private const string XmlNodeTextAtt = "text";
        private const string XmlNodeCheckedAtt = "checked";
        private const string XmlNodeExpandedAtt = "expanded";
        private const string XmlNodeTagAtt = "tag";
        private const string XmlNodeImageIndexAtt = "imageindex";

        private void SaveNodes(TreeNodeCollection nodesCollection, XmlTextWriter textWriter)
        {
            for (int i = 0; i < nodesCollection.Count; i++)
            {
                System.Windows.Forms.TreeNode node = nodesCollection[i];
                textWriter.WriteStartElement(XmlNodeTag);
                textWriter.WriteAttributeString(XmlNodeTextAtt,
                                                           node.Text);
                //add checked
                textWriter.WriteAttributeString(XmlNodeCheckedAtt,
                                                           node.Checked.ToString());

                //add expanded
                textWriter.WriteAttributeString(XmlNodeExpandedAtt,
                                                           node.IsExpanded.ToString());

                textWriter.WriteAttributeString(
                    XmlNodeImageIndexAtt, node.ImageIndex.ToString());
                if (node.Tag != null)
                {
                    TreeNodeCustomData customTagNode = (TreeNodeCustomData)node.Tag;
                    customTagNode.IsChecked = node.Checked;
                    textWriter.WriteAttributeString(XmlNodeTagAtt, customTagNode.CustomTagAsString);
                }
                // add other node properties to serialize here  
                if (node.Nodes.Count > 0)
                {
                    SaveNodes(node.Nodes, textWriter);
                }
                textWriter.WriteEndElement();
            }
        }
        
        #endregion Save File Selection to XML

        #region Load Selection From File
        public void LoadSavedSelection(string fileName,TreeView treeView, out string clientjobIdInfo)
        {
            clientjobIdInfo = "";
            if (File.Exists(fileName))
            {
                treeView.Nodes.Clear();
                try
                {
                    DeserializeTreeView(treeView, fileName, out clientjobIdInfo);
                }
                catch (System.Exception)
                {

                    //default to loading up a new file
                }
                
            }
        }
        private void DeserializeTreeView(TreeView treeView, string fileName, out string clientjobIdInfo)
        {
            XmlTextReader reader = null;
            clientjobIdInfo = "";
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
                            bool isEmptyElement = reader.IsEmptyElement;

                            // loading node attributes
                            int attributeCount = reader.AttributeCount;
                            if (attributeCount > 0)
                            {
                                for (int i = 0; i < attributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    SetAttributeValue(newNode,
                                                 reader.Name, reader.Value);
                                }
                            }
                            // add new node to Parent Node or TreeView
                            if (parentNode != null)
                                parentNode.Nodes.Add(newNode);
                            else
                                treeView.Nodes.Add(newNode);

                            // making current node 'ParentNode' if its not empty
                            if (!isEmptyElement)
                            {
                                parentNode = newNode;
                            }
                        }
                        else if(reader.Name == XmlJobFoldersTag)
                        {
                            int attributeCount = reader.AttributeCount;
                            if (attributeCount > 0)
                            {
                                for (int i = 0; i < attributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    var attrName = reader.Name;
                                    var attrValue = reader.Value;
                                    clientjobIdInfo = attrValue;
                                }
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
                        return;
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



        public EvoBoxFolder TransformXMLtoBoxFolderStructure(string folderConfigFile, IClientJobInfo clientInfo)
        {
            if (File.Exists(folderConfigFile))
            {
                string clientId, jobId;
                EvoBoxFolder folder =  DeserializeXMLToBoxFolder(folderConfigFile,out clientId, out jobId);
                clientInfo.CurrentSelectedClient = clientId;
                clientInfo.CurrentSelectedJobId = jobId;
                while(folder.Parent != null)
                {
                    folder = folder.Parent;
                }

                return TransformLocalBoxFolderStructureToCloudBoxFolderStructure(folder, clientInfo);
            }
            return null;
        }
        private EvoBoxFolder TransformLocalBoxFolderStructureToCloudBoxFolderStructure
            (EvoBoxFolder localFolderStructure, IClientJobInfo clientInfo)
        {
            EvoBoxFolder rootClientFolder = new EvoBoxFolder(clientInfo.CurrentSelectedClient);
            EvoBoxFolder jobIdFolder = new EvoBoxFolder(clientInfo.CurrentSelectedJobId);
            rootClientFolder.ChildFolders.Add(jobIdFolder);
            jobIdFolder.Parent = rootClientFolder;

            //the root folder of the local box folder structure should be tempRoot, 
            //each top directory folder should be ignored and it's children pointed at the jobIdFolder
            foreach(EvoBoxFolder topDirectoryFolder in localFolderStructure.ChildFolders)
            {
                foreach(EvoBoxFolder actualBoxFolders in topDirectoryFolder.ChildFolders)
                {
                    jobIdFolder.ChildFolders.Add(actualBoxFolders);
                    actualBoxFolders.Parent = jobIdFolder;
                }
            }

            return rootClientFolder;

        }

        private EvoBoxFolder DeserializeXMLToBoxFolder(string folderConfigFile, out string clientId, out string jobId)
        {

            EvoBoxFolder parentFolder = new EvoBoxFolder("TempRoot");
            XmlTextReader reader = null;
            try
            {
                clientId = "";
                jobId = "";
                reader = new XmlTextReader(folderConfigFile);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            bool isEmptyElement = reader.IsEmptyElement;
                            reader.MoveToAttribute(XmlNodeTextAtt);
                            var folderName = reader.Value;
                            reader.MoveToAttribute(XmlNodeCheckedAtt);
                            bool isChecked = true;
                            bool.TryParse(reader.Value, out isChecked);

                            reader.MoveToAttribute(XmlNodeTagAtt);
                            var tagInfo = new TreeNodeCustomData(reader.Value);

                            if(isChecked)
                            {
                                EvoBoxFolder newFolder = new EvoBoxFolder(folderName);
                                newFolder.CustomNodeTagData = tagInfo;
                                parentFolder.ChildFolders.Add(newFolder);
                                newFolder.Parent = parentFolder;
                                // depth first search
                                if (!isEmptyElement)
                                {
                                    parentFolder = newFolder;
                                }
                            }
                        }

                        else if (reader.Name == XmlJobFoldersTag)
                        {
                            int attributeCount = reader.AttributeCount;
                            if (attributeCount == 1)
                            {
                                reader.MoveToAttribute(0);
                                var clientjobIdInfo = reader.Value;
                                
                                RegexHelper.ExtractClientAndJobIds(clientjobIdInfo, out clientId, out jobId); 
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

                    }
                }
            }
            finally
            {
                reader.Close();
            }
            return parentFolder;
        }

        #endregion Load Selection From File
    }
}
