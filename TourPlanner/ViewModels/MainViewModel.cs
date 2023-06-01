using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.BL;
using TourPlanner.DAL.ElasticSearch;
using TourPlanner.Model;
using TourPlanner.UI.Views;

namespace TourPlanner.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
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

        private bool _isImageEnabled = true;
        public bool IsImageEnabled
        {
            get { return _isImageEnabled; }
            set
            {
                _isImageEnabled = value;
                OnPropertyChanged(nameof(IsImageEnabled));
            }
        }

        private bool _isDataGridVisible;
        public bool IsDataGridVisible
        {
            get { return _isDataGridVisible; }
            set
            {
                _isDataGridVisible = value;
                OnPropertyChanged(nameof(IsDataGridVisible));
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                Search();
            }
        }

        private ObservableCollection<ElasticTourDocument> _searchResults;
        public ObservableCollection<ElasticTourDocument> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }
        public ICommand ShowDetailsCommand { get; }
        public ICommand ShowLogDetailsCommand { get; }
        public ICommand MoreOptionsCommand { get; }

        public ICommand SearchCommand { get; }

        private TourLogsSideListBarViewModel tourLogBarVM;
        private TourSideListBarViewModel tourBarVM;
        private ITourPlannerManager bl;

        private static TourPlanner.DAL.Logging.ILoggerWrapper logger;

        public MainViewModel(ITourPlannerManager bl, TourLogsSideListBarViewModel tourLogBarVM, TourSideListBarViewModel tourBarVM)
        {
            this.bl = bl;
            this.tourLogBarVM = tourLogBarVM;
            this.tourBarVM = tourBarVM;
            logger = TourPlanner.DAL.Logging.LoggerFactory.GetLogger();
            ShowDetailsCommand = new RelayCommand(_ => ShowTourDetailView());
            ShowLogDetailsCommand = new RelayCommand(_ => ShowTourLogsDetailView());
            SearchCommand = new RelayCommand(_ => Search());
            if (tourBarVM.SelectedItem != null)
            {
                tourTitle = tourBarVM.SelectedItem.Name;
                TourImage = LoadImage(tourBarVM.SelectedItem.StaticMap);
            }
            IsDataGridVisible = false;
            tourBarVM.TourBar_SelectionChanged += (_, selected_Tour) => DisplayTourLogs(selected_Tour);
        }

        private void DisplayTourLogs(Tour tour)
        {
            logger.Debug("New logs get displayed");
            tourLogBarVM.SelectedTour = tour;

            if (tour != null)
            {
                TourTitle = tour.Name;
                tour.Logs = tourLogBarVM.Items;
                TourImage = LoadImage(tour.StaticMap);
            }
            else
            {
                logger.Warn("No tour selected");
                TourTitle = "";
                TourImage = null;
            }
        }



        public void ShowTourDetailView()
        {
            logger.Info($"{tourBarVM.SelectedItem.Name} detail view opened");
            TourDetailView tourDetailView = new TourDetailView
            {
                DataContext = new TourDetailViewModel(tourBarVM.SelectedItem)
            };
            tourDetailView.Show();
        }

        public void ShowTourLogsDetailView()
        {
            logger.Info("Log detail view opened");
            TourLogsDetailView tourLogsDetailView = new TourLogsDetailView
            {
                DataContext = new TourLogsDetailViewModel(tourLogBarVM.Items)
            };
            tourLogsDetailView.Show();
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            logger.Debug("Loading image");
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

        private void Search()
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Clear the search results and show the TourImage
                SearchResults = null;
                IsImageEnabled = true;
                IsDataGridVisible = false;
                TourImage = LoadImage(tourBarVM.SelectedItem.StaticMap);
                TourTitle = tourBarVM.SelectedItem.Name;

            }
            else
            {

                List<ElasticTourDocument> results = ElasticSearchService.Instance.FuzzySearch(searchText);
                TourTitle = "";
                TourImage = null;
                IsDataGridVisible = true;
                IsImageEnabled = false;

                SearchResults = new ObservableCollection<ElasticTourDocument>(results);
            }
        }

    }
}
