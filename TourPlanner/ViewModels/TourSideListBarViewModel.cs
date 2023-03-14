using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Model;
using TourPlanner.UI.Views;

namespace TourPlanner.UI.ViewModels
{
    class TourSideListBarViewModel : BaseViewModel, ISideListBarViewModel
    {
        private String listTitle = "Tours";

        public String ListTitle
        {
            get { return listTitle; }
            set { listTitle = value; }
        }

        private Tour selectedItem;
        public Tour SelectedItem 
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (value == selectedItem)
                    return;

                selectedItem = value;
                TourBar_SelectionChanged?.Invoke(this, SelectedItem);
            } 
        }

        public ObservableCollection<Tour> Items { get; set; } = new ObservableCollection<Tour>()
        {
            new Tour(){ Name="Tour 1", Description="Test 1", Distance=1.1, EstimatedTime=120, From="Wien", To="Südtirol", TourLogs={ new TourLog(){Name="Tour1Log1"}, new TourLog(){Name="Tour1Log2"}} },
            new Tour(){ Name="Tour 2", Description="Test 2", Distance=2.1, EstimatedTime=120, From="Brixen", To="Bozen", TourLogs={ new TourLog(){Name="Tour2Log1"}, new TourLog(){Name="Tour2Log2"}} }
        };

        public event EventHandler<Tour> TourBar_SelectionChanged;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }


        public TourSideListBarViewModel()
        {
            AddCommand = new RelayCommand(_ => AddItem());
            EditCommand = new RelayCommand(_ => EditItem());
            DeleteCommand = new RelayCommand(_ => DeleteItem());
        }

        public void AddItem()
        {
            AddNewTourForm addTourWindow = new AddNewTourForm();
            addTourWindow.Show();
        }

        public void EditItem()
        {
            MessageBox.Show("TourBar Edit");
        }

        public void DeleteItem()
        {
            MessageBox.Show("TourBar Delete");
        }
    }
}
