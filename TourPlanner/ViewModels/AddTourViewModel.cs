using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Model;
using System.Collections.ObjectModel;
using System;

namespace TourPlanner.UI.ViewModels
{
    class AddTourViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }

        public double Distance { get; set; }
        public ICommand AddTourCommand { get; set; }
        public ICommand CloseWindowCommand { get; }

        private ITourPlannerManager bl;
        private TourSideListBarViewModel vm;
        
        public AddTourViewModel(ITourPlannerManager bl, TourSideListBarViewModel vm)
        {
            AddTourCommand = new RelayCommand(_ => AddNewTour());

            this.bl = bl;
            this.vm = vm;
        }


        public void AddNewTour()
        {
            MessageBox.Show($"Name: {Name}, Description: {Description}, From: {From}, To: {To}, TransportType: {TransportType}, Distance: {Distance}");

            Tour tour = new Tour(Name, Description, From, To, Distance, 100);
            bl.AddTour(tour);
            vm.Items.Add(tour);
        }

        public void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
