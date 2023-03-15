using System;
using System.Collections.ObjectModel;

namespace TourPlanner.UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {

        private String tourTitle = "Heftige Tour";

        public String TourTitle
        {
            get { return tourTitle; }
            set { tourTitle = value; }
        }

        private TourLogsSideListBarViewModel tourLogBarVM;
        private TourSideListBarViewModel tourBarVM;
        public MainViewModel(TourLogsSideListBarViewModel tourLogBarVM, TourSideListBarViewModel tourBarVM)
        {
            this.tourLogBarVM = tourLogBarVM;
            this.tourBarVM = tourBarVM;
            tourBarVM.TourBar_SelectionChanged += (_, selected_Tour) => DisplayTourLogs(selected_Tour.TourLogs);
        }

        private void DisplayTourLogs(ObservableCollection<Model.TourLog> tourLogs)
        {
            tourLogBarVM.Items = tourLogs;
        }
    }
}
