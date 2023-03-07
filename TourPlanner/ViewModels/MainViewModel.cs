using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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

        private TourLogsSideListBarViewModel tourLogsSLBVM;
        private TourSideListBarViewModel tourSLBVM;
        public MainViewModel(TourLogsSideListBarViewModel tourLogsSLBVM, TourSideListBarViewModel tourSLBVM)
        {
            this.tourLogsSLBVM = tourLogsSLBVM;
            this.tourSLBVM = tourSLBVM;
        }
    }
}
