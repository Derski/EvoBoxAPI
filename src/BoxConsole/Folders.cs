using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BoxConsole
{
    public class Folders
    {
        public string BoxId { get; set; }
        public string FolderName { get; set; }

        public string BoxFolderName { get; set; }

        public string FullPath { get; set; }

        public string BoxParentId { get; set; }

        public Folders(string boxId, string boxFolderName, string boxParentId)
        {
            BoxId = boxId;
            BoxFolderName = boxFolderName;
            BoxParentId = boxParentId;
        }

    }

    public class FolderManager
    {


        public List<Folders> BuildFolderStructure(BoxCollection<BoxItem> f)
        {
            //look at this tomorrow
            //https://gist.github.com/cburnette/f58d30bf9d2edd2b6d80
            var root = f.Entries.FirstOrDefault(e => e.Parent.Id == "0");
            List<Folders> foldersList = null;
            if (root!= null)
            {
                foldersList = new List<Folders>();
                Folders rootFolder = new Folders(root.Id, root.Name, root.Parent.Id);
                foldersList.Add(rootFolder);
                LoopThroughBoxEntries(root, foldersList);
            }
            return foldersList;
        }

        private void  LoopThroughBoxEntries(BoxItem root, List<Folders> foldersList )
        {
            foreach (var child in root.PathCollection.Entries)
            {
                Folders childFolder = new Folders(child.Id, child.Name, child.Parent.Id);
                foldersList.Add(childFolder);
                LoopThroughBoxEntries(child, foldersList);
            }
        }
    }

}
