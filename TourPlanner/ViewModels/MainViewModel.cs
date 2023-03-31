using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.UI.Views;

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

        public ICommand ShowDetailsCommand { get; }
        public ICommand ShowLogDetailsCommand { get; }

        private TourLogsSideListBarViewModel tourLogBarVM;
        private TourSideListBarViewModel tourBarVM;
        private ITourPlannerManager bl;
        public MainViewModel(ITourPlannerManager bl, TourLogsSideListBarViewModel tourLogBarVM, TourSideListBarViewModel tourBarVM)
        {
            this.bl = bl;
            this.tourLogBarVM = tourLogBarVM;
            this.tourBarVM = tourBarVM;
            ShowDetailsCommand = new RelayCommand(_ => ShowTourDetailView());
            ShowLogDetailsCommand = new RelayCommand(_ => ShowTourLogsDetailView());

            if(tourBarVM.SelectedItem != null ) 
            {
                tourTitle = tourBarVM.SelectedItem.Name;
            }
            tourBarVM.TourBar_SelectionChanged += (_, selected_Tour) => DisplayTourLogs(selected_Tour);
        }

        private void DisplayTourLogs(Model.Tour tour)
        {

            tourLogBarVM.SelectedTour = tour;
            TourTitle = tour.Name;
        }

        public void ShowTourDetailView() 
        {
            TourDetailView tourDetailView = new TourDetailView
            {
                DataContext = new TourDetailViewModel(tourBarVM.SelectedItem)
            };
            tourDetailView.Show();
        }

        public void ShowTourLogsDetailView()
        {
            TourLogsDetailView tourLogsDetailView = new TourLogsDetailView
            {
                DataContext = new TourLogsDetailViewModel(tourLogBarVM.Items)
            };
            tourLogsDetailView.Show();
        }
    }
}
