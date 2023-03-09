using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Model;

namespace TourPlanner.UI.ViewModels
{
    class TourLogsSideListBarViewModel : BaseViewModel, ISideListBarViewModel
    {
        private String listTitle = "Tour Logs";

        public String ListTitle
        {
            get { return listTitle; }
            set { listTitle = value; }
        }

        private ObservableCollection<TourLog> items;
        public ObservableCollection<TourLog> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
                //OnPropertyChanged(Items);
            }
        }


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
