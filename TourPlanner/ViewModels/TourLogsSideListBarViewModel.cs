using System;
using System.Collections.ObjectModel;
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
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public TourLogsSideListBarViewModel()
        {
            AddCommand = new RelayCommand(_ => AddItem());
            EditCommand = new RelayCommand(_ => EditItem());
            DeleteCommand = new RelayCommand(_ => DeleteItem());
        }

        public void AddItem()
        {
            AddTourLogForm addTourLogWindow = new AddTourLogForm { DataContext = new AddTourLogViewModel() };
            addTourLogWindow.Show();
        }

        public void EditItem()
        {
            MessageBox.Show("TourLogBar Edit");
        }

        public void DeleteItem()
        {
            MessageBox.Show("TourLogBar Delete");
        }
    }
}
