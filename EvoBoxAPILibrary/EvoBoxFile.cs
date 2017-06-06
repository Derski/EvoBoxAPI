using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoBoxAPI
{
    public class EvoBoxFile
    {
        public string Extension { get; set; }
        public string FullLocalName { get; set; }
        public string LocalFileName { get; set; }

        public string EvoBoxFileId { get; set; }
        public string SHA1 { get; set; }

        public EvoBoxFile(string fullName)
        {
            FullLocalName = fullName;
        }
    }
}
