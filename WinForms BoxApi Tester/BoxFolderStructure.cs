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
        public static List<EvoBoxFolder> GetSampleLocalFolders()
        {
            List<EvoBoxFolder> LocalFolders = new List<EvoBoxFolder>();
            EvoBoxFolder rootJobFolder = new EvoBoxFolder(@"C:\Users\cderkowski\Desktop\JobWork", "JobWork",true);

            EvoBoxFolder decodedDataFolder = new EvoBoxFolder(@"C:\Users\cderkowski\Desktop\JobWork\DecodedData", "DecodedData",true);
            EvoBoxFolder sensorSimulatorFolder = new EvoBoxFolder(@"C:\Users\cderkowski\Desktop\JobWork\DecodedData", "EVO SensorSimulator",true);
            EvoBoxFolder rawSampleDataFolder = new EvoBoxFolder(@"C:\Users\cderkowski\Desktop\JobWork\RawSampleData", "RawSampleData",true);


            rootJobFolder.ChildFolders.Add(decodedDataFolder);
            rootJobFolder.ChildFolders.Add(sensorSimulatorFolder);
            rootJobFolder.ChildFolders.Add(rawSampleDataFolder);

            EvoBoxFolder rawDataFolder = new EvoBoxFolder(@"C:\RawData", "RawData",true);
            LocalFolders.Add(rootJobFolder);
            LocalFolders.Add(rawDataFolder);
            return LocalFolders;
        }

        public static List<EvoBoxFolder> CreateLocalEvoBoxFolderStructure(TreeNodeCollection nodes,string basePath)
        {
            DirectoryInfo i = new DirectoryInfo(basePath);
            string parentFolder = "c:\\";
            if (i.Parent != null)
            {
                parentFolder = i.Parent.FullName;
            }

            List<EvoBoxFolder> evoBoxFolders = new List<EvoBoxAPI.EvoBoxFolder>();
            foreach (TreeNode node in nodes)
            {
                if(node.Checked)
                {
                    EvoBoxFolder folder = new EvoBoxFolder(Path.Combine(basePath, node.FullPath), node.Text, node.Checked);
                    evoBoxFolders.Add(folder);
                    AddChildFolders(folder, node.Nodes, basePath);
                }
            }
            return evoBoxFolders;
        }

        private static void AddChildFolders(EvoBoxFolder parentFolder, TreeNodeCollection nodes, string basePath)
        {
            foreach(TreeNode childNode in nodes)
            {
                if(childNode.Checked)
                {
                    EvoBoxFolder childFolder =
                    new EvoBoxFolder(Path.Combine(basePath, childNode.FullPath), childNode.Text, childNode.Checked);
                    parentFolder.ChildFolders.Add(childFolder);
                    AddChildFolders(childFolder, childNode.Nodes, basePath);
                }
            }
            
        }
    }

}
