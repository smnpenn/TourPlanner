﻿using System.Windows;
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
            var bl = new TourPlannerManagerImpl(dal);

            var tourBarVM = new TourSideListBarViewModel(bl);
            var tourLogBarSLBVM = new TourLogsSideListBarViewModel(bl);
            if(tourBarVM.Items.Count > 0)
            {
                tourLogBarSLBVM.Items = bl.GetTourLogs(tourBarVM.Items[0]);
            }
            //tourLogBarSLBVM.Items = new System.Collections.ObjectModel.ObservableCollection<Model.TourLog>(tourBarVM.Items[0].TourLogs);

            var wnd = new MainWindow
            {
                DataContext = new MainViewModel(bl, tourLogBarSLBVM, tourBarVM),
                TourListBar = { DataContext = tourBarVM },
                TourLogsListBar = { DataContext = tourLogBarSLBVM },
            };
            wnd.Show();
        }

    }
}
