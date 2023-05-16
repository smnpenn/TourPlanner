using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TourPlanner.BL;
using TourPlanner.Model;
using TourPlanner.UI.Service;
using System.Windows;

namespace TourPlanner.UI.ViewModels
{
    public class EditLogViewModel : BaseViewModel
    {
        public string Name { get; set; }

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

        public int _rating;
        public int Rating
        {
            get { return _rating; }
            set
            {

                _rating = value;
                OnPropertyChanged(nameof(Rating));

            }
        }

        public ObservableCollection<Brush> Stars { get; set; }

        public int Time { get; set; }

        public double Difficulty { get; set; }

        public ICommand EditTourLogCommand { get; }
        public ICommand CloseWindowCommand { get; }
        public ICommand UpdateRatingCommand { get; }

        private ITourPlannerManager bl;
        private TourLogsSideListBarViewModel vm;
        //private Tour relatedTour;
        private DAL.Logging.ILoggerWrapper logger;
        private TourLog currentLog;

        public EditLogViewModel(TourLog log, ITourPlannerManager bl, TourLogsSideListBarViewModel vm)
        {
            EditTourLogCommand = new RelayCommand(_ => UpdateLog());
            CloseWindowCommand = new RelayCommand(_ => CloseWindow());
            UpdateRatingCommand = new RelayCommand(ChangeStar);
            this.bl = bl;
            this.vm = vm;
            //relatedTour = vm.SelectedTour;
            logger = DAL.Logging.LoggerFactory.GetLogger();

            if (log == null)
            {
                logger.Error("Edit Tour: given tour is null");
                MessageBox.Show("no tour provided");
                CloseWindow();
            }
            else
            {
                currentLog = log;
                Name = currentLog.Name;
                Date = currentLog.DateTime;
                Comment = currentLog.Comment;
                Rating = (int)currentLog.Rating;
                Time = currentLog.TotalTime;
                Difficulty = currentLog.Difficulty;
            }
        }

        public void UpdateLog()
        {
            if(currentLog != null)
            {
                currentLog.Name = Name;
                currentLog.Comment = Comment;
                currentLog.DateTime = Date;
                currentLog.Rating = Rating;
                currentLog.Difficulty = Difficulty;
                currentLog.TotalTime = Time;

                int index = vm.Items.IndexOf(vm.Items.Where(X => X.Id == currentLog.Id).First());
                if (index > -1)
                {
                    vm.Items[index] = currentLog;
                    bl.UpdateTourLog(currentLog);

                    MessageBox.Show("Update successful");
                    CloseWindow();
                }
                else
                {
                    logger.Error("Edit Log: could not update data");
                    MessageBox.Show("Error while updating data");
                }
            }
            else
            {
                logger.Error("Edit Log: unexpected error");
                MessageBox.Show("Error while updating data");
            }
        }

        public void CloseWindow()
        {
            DialogService.Instance.CloseDialog(this);
        }

        public void ChangeStar(object parameter)
        {

            if (Convert.ToInt32(parameter) is int starIndex)
            {
                Rating = starIndex;
            }
        }
    }
}
