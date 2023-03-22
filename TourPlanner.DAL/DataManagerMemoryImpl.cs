using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DAL
{
    public class DataManagerMemoryImpl : IDataManager
    {
        public ObservableCollection<Tour> GetTours()
        {
            return new ObservableCollection<Tour>()
        {
            new Tour(){ Name="Tour 1", Description="Test 1", Distance=1.1, EstimatedTime=120, From="Wien", To="Südtirol", TourLogs={ new TourLog(){Name="Tour1Log1"}, new TourLog(){Name="Tour1Log2"}} },
            new Tour(){ Name="Tour 2", Description="Test 2", Distance=2.1, EstimatedTime=120, From="Brixen", To="Bozen", TourLogs={ new TourLog(){Name="Tour2Log1"}, new TourLog(){Name="Tour2Log2"}} }
        };
        }
    }
}
