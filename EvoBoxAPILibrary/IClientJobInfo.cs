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
    }
}
