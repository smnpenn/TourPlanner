using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using TourPlanner.BL;
using TourPlanner.Model;
using TourPlanner.UI.Service;

namespace TourPlanner.UI.ViewModels
{
    class AddTourLogViewModel : BaseViewModel
    {

        public string Name { get; set; } = "Log";

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string Comment { get; set; }


        // Rating

        public double Rating { get; set; }

        // Rating New
        public int _rating;
        public int NewRating
        {
            get { return _rating; }
            set
            {

                _rating = value;
                OnPropertyChanged(nameof(NewRating));

            }
        }

        public ObservableCollection<Brush> Stars { get; set; }

        public int Time { get; set; }

        public double Difficulty { get; set; }

        public ICommand AddTourLogCommand { get; }
        public ICommand CloseWindowCommand { get; }
        public ICommand UpdateRatingCommand { get; }

        // Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private Tour relatedTour;

        private ITourPlannerManager bl;
        private TourLogsSideListBarViewModel vm;

        public AddTourLogViewModel(ITourPlannerManager bl, TourLogsSideListBarViewModel vm)
        {
            AddTourLogCommand = new RelayCommand(_ => AddNewTourLog());
            CloseWindowCommand = new RelayCommand(_ => CloseWindow());
            UpdateRatingCommand = new RelayCommand(ChangeStar);
            NewRating = 0;
            Date = DateTime.Now;
            this.bl = bl;
            this.vm = vm;
            relatedTour = vm.SelectedTour;
            // RatingNew = 0;

        }

        public void ChangeStar(object parameter)
        {

            if (Convert.ToInt32(parameter) is int starIndex)
            {
                NewRating = starIndex;
            }
        }

        public void AddNewTourLog()
        {
            TourLog log = new TourLog(Name, relatedTour, Date, Comment, Difficulty, Time, Convert.ToDouble(NewRating));
            bl.AddTourLog(log);
            vm.Items.Add(log);
            // TO-DO: Add TourLog to Tour -> We need to pass the current tour as parameter to the addNewTourLog to add the TourLog to the respective Tour
        }

        // Experimental, dont know how to pass window yet. 
        public void CloseWindow()
        {
            DialogService.Instance.CloseDialog(this);
        }
    }
}
