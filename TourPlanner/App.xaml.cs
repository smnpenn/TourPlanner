using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            var tourSLBVM = new TourSideListBarViewModel();
            var tourLogsSLBVM = new TourLogsSideListBarViewModel();
            tourLogsSLBVM.Items = tourSLBVM.Items[0].TourLogs;

            var wnd = new MainWindow
            {
                DataContext = new MainViewModel(tourLogsSLBVM, tourSLBVM),
                TourListBar = {DataContext = tourSLBVM},
                TourLogsListBar = {DataContext= tourLogsSLBVM},
            };
            wnd.Show();
        }
    }
}
