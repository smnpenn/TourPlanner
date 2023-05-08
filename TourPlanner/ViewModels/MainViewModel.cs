using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.BL;
using TourPlanner.Model;
using TourPlanner.UI.Service;
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
        public ICommand MoreOptionsCommand { get; }

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
            //MoreOptionsCommand = new RelayCommand(_ => ExportData());
            MoreOptionsCommand = new RelayCommand(_ => GenerateTourReport());

            if (tourBarVM.SelectedItem != null)
            {
                tourTitle = tourBarVM.SelectedItem.Name;
                TourImage = LoadImage(tourBarVM.SelectedItem.StaticMap);
            }
            tourBarVM.TourBar_SelectionChanged += (_, selected_Tour) => DisplayTourLogs(selected_Tour);
        }

        private void DisplayTourLogs(Tour tour)
        {

            tourLogBarVM.SelectedTour = tour;


            if (tour != null)
            {
                TourTitle = tour.Name;
                tour.Logs = tourLogBarVM.Items;
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

        public void ExportData()
        {
            bl.ExportData(tourBarVM.Items, "test");
        }

        public void GenerateTourReport()
        {
            if (tourBarVM.SelectedItem == null)
            {
                MessageBox.Show("Please select the tour you want to create the report about.");
                return;
            }

            string? filename = DialogService.Instance.ShowSaveFileDialog($"Tour{tourBarVM.SelectedItem.Id}_Report", "pdf");

            // Process save file dialog box results
            if (filename != null)
            {
                bl.GenerateTourReport(tourBarVM.SelectedItem, filename);
                DialogService.Instance.OpenFileExplorer(filename);
            }
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
