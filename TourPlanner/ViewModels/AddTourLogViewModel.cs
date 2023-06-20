using System;
using System.Collections.Generic;
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

        // Input validation

        private string? _errors;
        public string? Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                OnPropertyChanged(nameof(Errors));
            }
        }

        private List<string> GetValidationErrors()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(Name))
            {
                errors.Add("Name cannot be empty.");
            }

            if (string.IsNullOrEmpty(Comment))
            {
                errors.Add("Comment cannot be empty.");
            }
            if (NewRating < 1)
            {
                errors.Add("Rating cannot be lower than 1 star");
            }
            if (Difficulty < 1)
            {
                errors.Add("Difficulty must be greater than 0");
            }
            if (Time < 1)
            {
                errors.Add("Total time must be greater than 0");
            }

            // Add validation checks for other properties as needed

            return errors;
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
            Errors = string.Join(Environment.NewLine, GetValidationErrors());
            if (string.IsNullOrEmpty(Errors))
            {
                bl.AddTourLog(log);
                vm.Items.Add(log);
                bl.CalculateAdditionalAttributes(relatedTour);
            }

        }

        public void CloseWindow()
        {
            DialogService.Instance.CloseDialog(this);
        }
    }
}
