using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.UI.ViewModels
{
    class TourLogsSideListBarViewModel : ISideListBarViewModel
    {
        private String listTitle = "Tour Logs";

        public String ListTitle
        {
            get { return listTitle; }
            set { listTitle = value; }
        }

        public ObservableCollection<TourLogs> Items { get; set; } = new ObservableCollection<TourLogs>()
        {
            new TourLogs(){Name="Log 1", Comment= "Comment 1", Rating=5, Difficulty="Hard"},
            new TourLogs(){Name="Log 2", Comment= "Comment 1", Rating=5, Difficulty="Hard"}
        };

        public void AddItem()
        {
            throw new NotImplementedException();
        }

        public void EditItem()
        {
            throw new NotImplementedException();
        }

        public void RemoveItem()
        {
            throw new NotImplementedException();
        }
    }
}
