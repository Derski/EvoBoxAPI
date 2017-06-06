using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvoBoxAPI;
namespace EvoBoxAPILibrary
{
    public class ClientJobInfoStub : IClientJobInfo
    {
        List<string> clientMockUps;
        Dictionary<string, List<string>> ClientJobs;

        public ClientJobInfoStub()
        {
            clientMockUps = new List<string>();
            clientMockUps.Add("Phoenix");
            clientMockUps.Add("InPetro");
            clientMockUps.Add("Evo");

            ClientJobs = new Dictionary<string, List<string>>();
            List<string> PhoenixJobs = new List<string>();
            PhoenixJobs.Add("P-Job1");
            PhoenixJobs.Add("P-Job2");
            PhoenixJobs.Add("P-Job3");
            ClientJobs.Add("Phoenix", PhoenixJobs);

            List<string> InPetroJobs = new List<string>();
            InPetroJobs.Add("I-Job1");
            InPetroJobs.Add("I-Job2");
            ClientJobs.Add("InPetro", InPetroJobs);

            List<string> EvoJobs = new List<string>();
            EvoJobs.Add("EV-Job1");
            EvoJobs.Add("EV-Job2");
            ClientJobs.Add("Evo", EvoJobs);
        }
        public List<string> GetClientList()
        {   
            return clientMockUps;
        }

        public List<string> GetJobListPerClient(string ClientId)
        {
            return ClientJobs[ClientId];
        }
    }
}
