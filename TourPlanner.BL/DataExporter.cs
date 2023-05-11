using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;
using System.IO;

namespace TourPlanner.BL
{
    public class DataExporter
    {
        public DataExporter() { }

        public void ExportData(ObservableCollection<Tour> data, string path)
        {
            
            string result = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, result);
        }
    }
}
