using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BL
{
    public class DataExporter
    {

        private string PATH_EXPORT = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/exports/";
        public DataExporter() { }

        public void ExportData(ObservableCollection<Tour> data, string filename)
        {
            string result = JsonConvert.SerializeObject(data);
            File.WriteAllText(PATH_EXPORT + filename + ".json", result);
        }
    }
}
