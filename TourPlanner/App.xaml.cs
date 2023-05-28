using System.Windows;
using TourPlanner.BL;
using TourPlanner.DAL;
using TourPlanner.UI.Service;
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
            //create all layers
            var dal = new DataManagerEFM(false);
            var bl = new TourPlannerManager(dal);

            var tourBarVM = new TourSideListBarViewModel(bl);
            var tourLogsVM = new TourLogsSideListBarViewModel(bl);
            var moreOptionsVM = new MoreOptionsViewModel(bl, tourBarVM, tourLogsVM);
            if(tourBarVM.SelectedItem != null)
            {
                tourLogsVM.Items = bl.GetTourLogs(tourBarVM.SelectedItem);
                tourLogsVM.SelectedTour = tourBarVM.SelectedItem;
                tourBarVM.SelectedItem.Logs = tourLogsVM.Items;
            }
           
            var wnd = new MainWindow
            {
                DataContext = new MainViewModel(bl, tourLogsVM, tourBarVM),
                TourListBar = { DataContext = tourBarVM },
                TourLogsListBar = { DataContext = tourLogsVM },
                MoreOptionsButton = { DataContext = moreOptionsVM },
                
            };
            wnd.Show();
        }

    }
}
