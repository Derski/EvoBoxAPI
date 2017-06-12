using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;

namespace EvoBoxAPILibrary.LogFinderService
{
    public class LocalLogLocationFinder : ILocalLogLocationFinder
    {
        /// <summary>
        /// is this already solved?
        /// </summary>
        /// <returns></returns>
        public string FindCurrentUserName()
        {
            var userName = "";
            try
            {
                userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                if(userName.ToLower().Contains("system") || userName.ToLower().Contains("service"))
                {

                }
            }
            catch (Exception)
            {

                throw;
            }


            return userName;
        }

        private string TryGettingJobWorkFolderUserNameByDirectorySearch()
        {
            string userName = "";
            string rootUserPath = @"c:\users";
            List<string> foundJobWorkFolders = new List<string>();
            if (Directory.Exists(rootUserPath))
            {
                DirectoryInfo usersInfo = new DirectoryInfo(rootUserPath);
                try
                {
                    foreach (var userDir in usersInfo.GetDirectories())
                    {
                        var currentUsersJobWork = Path.Combine(userDir.FullName, @"Desktop\JobWork");
                        if (Directory.Exists(currentUsersJobWork))
                        {
                            foundJobWorkFolders.Add(currentUsersJobWork);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            //TODO will there ever be more than 1? should return most recently edited one
            var jobWorkFolder =  foundJobWorkFolders.FirstOrDefault();
            if(jobWorkFolder != null)
            {

            }
            return userName;
        }

        private string GetServiceUserName()
        {
            throw new Exception("Not Working yet");
            string user = "";
            //ManagementObject wmiService = new ManagementObject("Win32_Service.Name='" + "EVOBackground" + "'");
            //wmiService.Get();
            //string user = wmiService["startname"].ToString();
            return user;
        }
    }
}
