using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Model;
using System.Collections.ObjectModel;
using System;
using System.Windows.Documents;
using System.Collections.Generic;
using TourPlanner.UI.Service;
using System.Linq;

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

            if(tour != null)
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
