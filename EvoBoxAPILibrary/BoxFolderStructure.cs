using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvoBoxAPI
{
    public static class BoxFolderStructure
    {
        public static EvoBoxFolder CreateLocalEvoBoxFolderStructure
            (TreeNodeCollection nodes,string clientId, string jobId)
        {
            List<EvoBoxFolder> evoBoxFolders = new List<EvoBoxAPI.EvoBoxFolder>();
            foreach (TreeNode node in nodes)
            {
                if(node.Checked)
                {
                    var firstBoxBode = node.Nodes[0];
                    EvoBoxFolder folder = new EvoBoxFolder(firstBoxBode.FullPath, firstBoxBode.Text, firstBoxBode.Checked);
                    evoBoxFolders.Add(folder);
                    AddChildFolders(folder, firstBoxBode.Nodes);
                }
            }
            //create jobId Folder and Client Folder
            string jobIdPrefix = clientId + "_" + jobId;

            EvoBoxFolder ClientJobPrefixFolder = new EvoBoxFolder(jobIdPrefix);
            ClientJobPrefixFolder.ChildFolders.AddRange(evoBoxFolders);

            EvoBoxFolder ClientRootFolder = new EvoBoxFolder(clientId);
            ClientRootFolder.ChildFolders.Add(ClientJobPrefixFolder);


            return ClientRootFolder;
        }

        private static void AddChildFolders(EvoBoxFolder parentFolder, TreeNodeCollection nodes)
        {
            foreach(TreeNode childNode in nodes)
            {
                if(childNode.Checked)
                {
                    EvoBoxFolder childFolder =
                    new EvoBoxFolder(childNode.FullPath, childNode.Text, childNode.Checked);
                    parentFolder.ChildFolders.Add(childFolder);
                    AddChildFolders(childFolder, childNode.Nodes);
                }
            }
            
        }
    }

}
