using System.Windows;
using TourPlanner.BL;
using TourPlanner.DAL;
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
            var dal = new DataManagerEFM();
            var bl = new TourPlannerManager(dal);

            var tourBarVM = new TourSideListBarViewModel(bl);
            var tourLogBarSLBVM = new TourLogsSideListBarViewModel(bl);
            var moreOptionsVM = new MoreOptionsViewModel();
            if(tourBarVM.SelectedItem != null)
            {
                tourLogBarSLBVM.Items = bl.GetTourLogs(tourBarVM.SelectedItem);
                tourLogBarSLBVM.SelectedTour = tourBarVM.SelectedItem;
                tourBarVM.SelectedItem.Logs = tourLogBarSLBVM.Items;
            }
           
            var wnd = new MainWindow
            {
                DataContext = new MainViewModel(bl, tourLogBarSLBVM, tourBarVM),
                TourListBar = { DataContext = tourBarVM },
                TourLogsListBar = { DataContext = tourLogBarSLBVM },
                MoreOptionsButton = { DataContext = moreOptionsVM },
                
            };
            wnd.Show();
        }

    }
}
