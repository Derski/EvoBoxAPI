using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoBoxAPILibrary
{
    public interface IFileManager
    {
        void GetBoxFileInfoForBoxFolders(EvoBoxFolder folder);
        void GetBoxFileInfoForBoxFolders(string evoBoxFolderId, List<EvoBoxFile> boxFiles);
    }
}
