﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.BL;
using TourPlanner.Model;
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

        private BitmapImage tourImage;
        public BitmapImage TourImage 
        {
            get { return tourImage; }
            set
            {
                tourImage = value;
                OnPropertyChanged(nameof(tourImage));
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
                TourImage = LoadImage(tourBarVM.SelectedItem.StaticMap);
            }
            tourBarVM.TourBar_SelectionChanged += (_, selected_Tour) => DisplayTourLogs(selected_Tour);
        }

        private void DisplayTourLogs(Model.Tour tour)
        {

            tourLogBarVM.SelectedTour = tour;

            if(tour != null)
            {
               TourTitle = tour.Name;
               TourImage = LoadImage(tour.StaticMap);
            }
            else
            {
                TourTitle = "";
                TourImage = null;
            }
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

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
