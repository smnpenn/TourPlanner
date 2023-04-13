using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Model;
using TourPlanner.UI.Service;
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

        public ObservableCollection<Tour> Items { get; set; }

        public event EventHandler<Tour> TourBar_SelectionChanged;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private ITourPlannerManager bl;
        private AddTourViewModel addTourVM;

        private readonly DialogService _dialogService;
        public TourSideListBarViewModel(ITourPlannerManager bl)
        {
            this.bl = bl;
            _dialogService = new DialogService();
            AddCommand = new RelayCommand(_ => AddItem());
            EditCommand = new RelayCommand(_ => EditItem());
            DeleteCommand = new RelayCommand(_ => DeleteItem());
            
            Items = bl.GetTours();
            if(Items.Count > 0)
            {
                SelectedItem = Items[0];
            }

            addTourVM = new AddTourViewModel(bl, this);

        }

        public void AddItem()
        {
            /**
            AddNewTourForm addTourWindow = new AddNewTourForm
            {
                DataContext = addTourVM
            };
            addTourWindow.Show();
            **/

            _dialogService.ShowDialog<AddNewTourForm>(addTourVM); // Hier muss halt immer das ViewModel dazugegeben werden, da ansonsten nur die nackte XAML geladen wird.
           
        }

        public void EditItem()
        {
            bl.GetRoute(selectedItem);
        }

        public void DeleteItem()
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Please select the tour you want to delete.");
                return;
            }
            MessageBoxResult result;
            result = MessageBox.Show($"Are you sure you want to delete \"{selectedItem.Name}\"?", "Test", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {
                //delete in DB
                bl.DeleteTour(SelectedItem);
                Items.Remove(SelectedItem);
            }
        }
    }
}
