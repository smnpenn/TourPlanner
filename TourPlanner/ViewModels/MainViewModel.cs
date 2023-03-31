using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TourPlanner.BL;

namespace TourPlanner.UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {

        private String tourTitle;

        public String TourTitle
        {
            get { return tourTitle; }
            set 
            {
                tourTitle = value;
                OnPropertyChanged(nameof(tourTitle));
            }
        }

        private TourLogsSideListBarViewModel tourLogBarVM;
        private TourSideListBarViewModel tourBarVM;
        private ITourPlannerManager bl;
        public MainViewModel(ITourPlannerManager bl, TourLogsSideListBarViewModel tourLogBarVM, TourSideListBarViewModel tourBarVM)
        {
            this.bl = bl;
            this.tourLogBarVM = tourLogBarVM;
            this.tourBarVM = tourBarVM;

            if (tourBarVM.Items.Count  > 0)
            {
                TourTitle = tourBarVM.Items[0].Name;
            }
            
            tourBarVM.TourBar_SelectionChanged += (_, selected_Tour) => DisplayTourLogs(selected_Tour);
        }

        private void DisplayTourLogs(Model.Tour tour)
        {

            tourLogBarVM.SelectedTour = tour;
            TourTitle = tour.Name;
        }
    }
}
