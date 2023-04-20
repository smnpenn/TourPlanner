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
        public DataExporter() { }

        public void ExportData(ObservableCollection<Tour> data, string filename)
        {
            string result = JsonConvert.SerializeObject(data);
        }
    }
}
