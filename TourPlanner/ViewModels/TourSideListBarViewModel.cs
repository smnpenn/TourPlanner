﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Model;
using TourPlanner.UI.Service;
using TourPlanner.UI.Views;

namespace TourPlanner.UI.ViewModels
{
    public class TourSideListBarViewModel : BaseViewModel, ISideListBarViewModel
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
        public TourSideListBarViewModel(ITourPlannerManager bl)
        {
            this.bl = bl;
            AddCommand = new RelayCommand(_ => AddItem());
            EditCommand = new RelayCommand(_ => EditItem());
            DeleteCommand = new RelayCommand(_ => DeleteItem());
            
            Items = bl.GetTours();

            foreach(Tour tour in Items)
            {
                bl.CalculateAdditionalAttributes(tour);
            }

            if(Items.Count > 0)
            {
                SelectedItem = Items[0];
            }
        }

        public void AddItem()
        {
            AddTourViewModel addTourVM = new AddTourViewModel(bl, this);
            DialogService.Instance.ShowDialog<AddNewTourForm>(addTourVM);
        }

        public void EditItem()
        {

            if (selectedItem == null)
            {
                MessageBox.Show("Please select the tour you want to edit.");
                return;
            }

            EditTourViewModel editTourVM = new EditTourViewModel(selectedItem, bl, this);
            DialogService.Instance.ShowDialog<EditTourForm>(editTourVM);
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
