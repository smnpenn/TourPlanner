﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
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

        private Tour selectedTour;
        public Tour SelectedTour 
        {
            get
            {
                return selectedTour;
            }
            set
            {
                if(value == null)
                {
                    Items = null;
                    return;
                }
                selectedTour = value;
                Items = selectedTour.TourLogs;
            } 
        }

        private TourLog selectedItem;
        public TourLog SelectedItem
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
            }
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

        private ITourPlannerManager bl;

        public TourLogsSideListBarViewModel(ITourPlannerManager bl)
        {
            AddCommand = new RelayCommand(_ => AddItem());
            EditCommand = new RelayCommand(_ => EditItem());
            DeleteCommand = new RelayCommand(_ => DeleteItem());

            this.bl = bl;
        }

        public void AddItem()
        {
            AddTourLogForm addTourLogWindow = new AddTourLogForm 
            { 
                DataContext = new AddTourLogViewModel(bl, SelectedTour) 
            };
            addTourLogWindow.Show();
        }

        public void EditItem()
        {
            MessageBox.Show("TourLogBar Edit");
        }

        public void DeleteItem()
        {
            if(selectedItem == null)
            {
                MessageBox.Show("Please select the log you want to delete.");
                return;
            }
            MessageBoxResult result;
            result = MessageBox.Show($"Are you sure you want to delete \"{selectedItem.Name}\"?", "Test", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            
            if(result == MessageBoxResult.Yes)
            {
                //delete in DB
                bl.DeleteTourLog(SelectedItem);
            }
        }
    }
}
