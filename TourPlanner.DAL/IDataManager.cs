using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DAL
{
    public interface IDataManager
    {
        public ObservableCollection<Tour> GetTours();
    }
}
