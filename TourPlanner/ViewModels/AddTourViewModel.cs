using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.DAL.ElasticSearch;
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

            if (tour != null)
            {
                bl.AddTour(tour);

                var res = await ElasticSearchService.Instance.IndexTourDocument(tour);
                if (res != null)
                {
                    logger.Debug("AddTour: add tour");
                    vm.Items.Add(tour);
                    CloseWindow();
                }
                else
                {
                    Console.WriteLine("An error occured when adding the document");
                }
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
