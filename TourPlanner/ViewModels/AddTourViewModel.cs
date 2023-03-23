using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Model;
using System.Collections.ObjectModel;

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
        public AddTourViewModel(ITourPlannerManager bl)
        {
            AddTourCommand = new RelayCommand(_ => AddNewTour());

            this.bl = bl;
        }


        public void AddNewTour()
        {
            MessageBox.Show($"Name: {Name}, Description: {Description}, From: {From}, To: {To}, TransportType: {TransportType}, Distance: {Distance}");

            Tour tour = new Tour(Name, Description, From, To, Distance, 100, new ObservableCollection<TourLog>());
            bl.AddTour(tour);
        }

        public void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
