using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BL
{
    public class DataImporter
    {

        public DataImporter() { }

        public List<Tour> ImportData(Stream fileStream) 
        {
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string json = reader.ReadToEnd();
                List<Tour> data = JsonConvert.DeserializeObject<List<Tour>>(json);

                return data;
            }
        }
    }
}
