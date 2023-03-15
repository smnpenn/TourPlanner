using System.Windows;
using TourPlanner.UI.ViewModels;
using TourPlanner.UI.Views;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var tourBarVM = new TourSideListBarViewModel();
            var tourLogBarSLBVM = new TourLogsSideListBarViewModel();
            tourLogBarSLBVM.Items = tourBarVM.Items[0].TourLogs;

            var wnd = new MainWindow
            {
                DataContext = new MainViewModel(tourLogBarSLBVM, tourBarVM),
                TourListBar = { DataContext = tourBarVM },
                TourLogsListBar = { DataContext = tourLogBarSLBVM },
            };
            wnd.Show();
        }

    }
}
