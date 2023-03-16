using System.Windows;
using System.Windows.Input;

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


        public AddTourViewModel()
        {
            AddTourCommand = new RelayCommand(_ => AddNewTour());


        }


        public void AddNewTour()
        {
            MessageBox.Show($"Name: {Name}, Description: {Description}, From: {From}, To: {To}, TransportType: {TransportType}, Distance: {Distance}");


        }

        public void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
