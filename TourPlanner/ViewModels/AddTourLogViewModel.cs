using System.Windows;
using System.Windows.Input;

namespace TourPlanner.UI.ViewModels
{
    class AddTourLogViewModel : BaseViewModel
    {

        public string Date { get; set; }

        public string Comment { get; set; }

        public double Rating { get; set; }

        public int Time { get; set; }

        public double Difficulty { get; set; }

        public ICommand AddTourLogCommand { get; }
        public ICommand CloseWindowCommand { get; }
        // Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);


        public AddTourLogViewModel()
        {
            AddTourLogCommand = new RelayCommand(_ => AddNewTourLog());
            // CloseWindowCommand = new RelayCommand(_ => CloseWindow(win)); --> closes windows below somehow
        }


        public void AddNewTourLog()
        {

            MessageBox.Show($"Date: {Date}, Comment: {Comment}, Rating: {Rating}, Time: {Time}, Difficulty: {Difficulty}");

            // TO-DO: Add TourLog to Tour -> We need to pass the current tour as parameter to the addNewTourLog to add the TourLog to the respective Tour



        }

        // Experimental, dont know how to pass window yet. 
        public void CloseWindow(Window window)
        {
            window.Close();
        }

    }
}
