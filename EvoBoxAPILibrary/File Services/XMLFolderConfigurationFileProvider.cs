using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoBoxAPILibrary.File_Services
{
    public static class XMLFolderConfigurationFileProvider
    {
        public const string xmlFolderStructureFileName = "FolderStructureConfiguration.xml";

        public  static string FolderStructureFileFullPath
        {
            get
            {
                var _file = Path.Combine
                    (Environment.CurrentDirectory, xmlFolderStructureFileName);
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    _file = TryGetDeploymentFileName();

                }
                return _file;
            }
        }

        private static string TryGetDeploymentFileName()
        {
            string fileName = "";
            try
            {
                fileName = Path.Combine(ApplicationDeployment.CurrentDeployment.DataDirectory, xmlFolderStructureFileName);
            }
            catch (Exception ex)
            {
                
            }
            return fileName;
        }
    }
}
