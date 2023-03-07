using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.UI.ViewModels
{
    class TourSideListBarViewModel : ISideListBarViewModel
    {
        private String listTitle = "Tours";

        public String ListTitle
        {
            get { return listTitle; }
            set { listTitle = value; }
        }

        public ObservableCollection<Tour> Items { get; set; } = new ObservableCollection<Tour>()
        {
            new Tour(){ Name="Tour 1", Description="Test 1", Distance=1.1, EstimatedTime=120, From="Wien", To="Südtirol"},
            new Tour(){ Name="Tour 2", Description="Test 2", Distance=2.1, EstimatedTime=120, From="Brixen", To="Bozen"}
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
