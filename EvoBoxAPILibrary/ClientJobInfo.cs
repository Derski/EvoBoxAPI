using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoBoxAPI
{
    public class ClientJobInfo : IClientJobInfo
    {
        public string GetClientName
        {
            get
            {
                //todo should return tblJob.Client
                return "Phoenix";
            }
        }

        public string GetJobId
        {
            get
            {
                //todo should return tblJob.JObId +"_"+ tblJob.CreatedTime("yymmddhh")
                return "123";
            }
        }
    }
}
