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
    class TourLogsSideListBarViewModel : ISideListBarViewModel
    {
        private String listTitle = "Tour Logs";

        public String ListTitle
        {
            get { return listTitle; }
            set { listTitle = value; }
        }

        public ObservableCollection<TourLog> Items { get; set; }

        public void SideBar_SelectedItemChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Yooo");
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
