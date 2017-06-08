using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoBoxAPILibrary
{
    public interface IClientJobInfo
    {
        List<string> GetClientList();

        List<string> GetJobListPerClient(string ClientId);

        string CurrentSelectedClient { get; set; }

        string CurrentSelectedJobId { get; set; }

        string GetBoxClientJobIdPrefix { get; }
        string GetBoxClientJobIdRootFolderName { get; }
    }
}
