using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
