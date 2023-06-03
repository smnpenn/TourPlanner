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

                if(!ValidateData(data))
                {
                    throw new InvalidDataException("import file contained invalid data");
                }

                return data;
            }
        }

        private bool ValidateData(List<Tour> data)
        {
            if(data == null)
            {
                return false;
            }

            foreach (Tour t in data)
            {
                if(t.Name == null || t.To == null || t.From == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
