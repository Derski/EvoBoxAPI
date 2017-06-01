using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoBoxAPI
{
    public static class CreateTestFolderStructure
    {
        public static List<EvoBoxFolder> GetSampleLocalFolders()
        {
            List<EvoBoxFolder> LocalFolders = new List<EvoBoxFolder>();
            EvoBoxFolder rootJobFolder = new EvoBoxFolder(@"C:\Users\cderkowski\Desktop\JobWork", "JobWork");

            EvoBoxFolder decodedDataFolder = new EvoBoxFolder(@"C:\Users\cderkowski\Desktop\JobWork\DecodedData", "DecodedData");
            EvoBoxFolder sensorSimulatorFolder = new EvoBoxFolder(@"C:\Users\cderkowski\Desktop\JobWork\DecodedData", "EVO SensorSimulator");
            EvoBoxFolder rawSampleDataFolder = new EvoBoxFolder(@"C:\Users\cderkowski\Desktop\JobWork\RawSampleData", "RawSampleData");


            rootJobFolder.ChildFolders.Add(decodedDataFolder);
            rootJobFolder.ChildFolders.Add(sensorSimulatorFolder);
            rootJobFolder.ChildFolders.Add(rawSampleDataFolder);

            EvoBoxFolder rawDataFolder = new EvoBoxFolder(@"C:\RawData", "RawData");
            LocalFolders.Add(rootJobFolder);
            LocalFolders.Add(rawDataFolder);
            return LocalFolders;
        }
    }

}
