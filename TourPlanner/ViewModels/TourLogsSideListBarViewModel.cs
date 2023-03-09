using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Model;
using TourPlanner.UI.Views;

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

        private ObservableCollection<TourLog> items = new ObservableCollection<TourLog>();
        public ObservableCollection<TourLog> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ICommand AddCommand { get; }

        public TourLogsSideListBarViewModel()
        {
            AddCommand = new RelayCommand(new Action<object>(AddItem));
        }

        public void AddItem(object obj)
        {
            AddTourLogForm addTourLogWindow = new AddTourLogForm();
            addTourLogWindow.Show();
        }

        public void EditItem(object obj)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
