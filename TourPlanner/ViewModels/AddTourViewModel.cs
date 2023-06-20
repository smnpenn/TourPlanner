using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Model;
using TourPlanner.UI.Service;

namespace TourPlanner.UI.ViewModels
{
    class AddTourViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public TransportType TransportType { get; set; }
        public IEnumerable<TransportType> TransportValues
        {
            get
            {
                return Enum.GetValues(typeof(TransportType)).Cast<TransportType>();
            }
        }


        // Input validation
        // input validation
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

        private string GetValidationErrors()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(Name))
            {
                errors.Add("Name");
            }

            if (string.IsNullOrEmpty(Description))
            {
                errors.Add("Description");
            }
            if (string.IsNullOrEmpty(From))
            {
                errors.Add("From");
            }
            if (string.IsNullOrEmpty(To))
            {
                errors.Add("To");
            }

            if (errors.Count > 0)
            {
                string errorFormat = "The following field(s) are empty: {0}";
                return string.Format(errorFormat, string.Join(", ", errors));

            }
            // Add validation checks for other properties as needed

            return string.Empty;
        }

        public ICommand AddTourCommand { get; set; }
        public ICommand CloseWindowCommand { get; }

        private ITourPlannerManager bl;
        private TourSideListBarViewModel vm;
        private DAL.Logging.ILoggerWrapper logger;
        public AddTourViewModel(ITourPlannerManager bl, TourSideListBarViewModel vm)
        {
            AddTourCommand = new RelayCommand(_ => AddNewTour());
            CloseWindowCommand = new RelayCommand(_ => CloseWindow());
            this.bl = bl;
            this.vm = vm;
            logger = DAL.Logging.LoggerFactory.GetLogger();

            Name = "";
            Description = "";
            From = "";
            To = "";
            TransportType = 0;
        }
        public async void AddNewTour()
        {

            Tour tour = new Tour(Name, Description, From, To, TransportType);
            tour = await bl.GetRoute(tour);

            Errors = GetValidationErrors();

            if ((tour != null) && (string.IsNullOrEmpty(Errors)))
            {

                bl.AddTour(tour);
                vm.Items.Add(tour);
                logger.Debug("AddTour: add tour");
                CloseWindow();
            }
            else
            {
                logger.Error("AddTour: could not create route");
                MessageBox.Show("Error creating Route!");
            }
        }

        public void CloseWindow()
        {
            DialogService.Instance.CloseDialog(this);
        }
    }
}
