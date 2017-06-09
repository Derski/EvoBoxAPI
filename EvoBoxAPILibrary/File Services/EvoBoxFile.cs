using Box.V2.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoBoxAPILibrary.File_Services
{
    public class EvoBoxFile
    {
        public string Extension
        {
            get
            {
                return Path.GetExtension(FullLocalName);
            }
        }
        public string FullLocalName { get; set; }

        public string LocalFileName
        {
            get
            {
               return  Path.GetFileName(FullLocalName);
            }
        }

        public bool MostRecentAlreadyUploaded
        {
            get
            {
                if(SHA1_Box == null)
                {
                    return false;
                }
                return
                String.Equals(SHA1_Box, SHA1_Local,
                   StringComparison.OrdinalIgnoreCase);
            }
        }


        public string EvoBoxFileId { get; set; }

        private string _sha1_Local;
        public string SHA1_Local
        {
            get
            {
                if(_sha1_Local == null)
                {
                    _sha1_Local = SetLocalSHA1Hash(FullLocalName);
                }
                return _sha1_Local;
            }
            set
            {
                _sha1_Local = value;
            }
        }

        public string SHA1_Box { get; set; }

        public EvoBoxFile(string fullName)
        {
            FullLocalName = fullName;
        }
        private static string SetLocalSHA1Hash(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (var cryptoProvider = new SHA1CryptoServiceProvider())
                {
                    string hash = BitConverter.ToString(cryptoProvider.ComputeHash(fs));
                    string hexHash = hash.Replace("-", "");
                    return hexHash.ToLower();
                }
            }
        }
    }
}
