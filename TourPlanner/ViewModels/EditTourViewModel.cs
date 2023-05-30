using System.Linq;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.DAL.ElasticSearch;
using TourPlanner.Model;
using TourPlanner.UI.Service;

namespace TourPlanner.UI.ViewModels
{
    public class EditTourViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICommand EditTourCommand { get; set; }
        public ICommand CloseWindowCommand { get; }

        private ITourPlannerManager bl;
        private DAL.Logging.ILoggerWrapper logger;
        private Tour currentTour;
        private TourSideListBarViewModel vm;

        public EditTourViewModel(Tour tour, ITourPlannerManager bl, TourSideListBarViewModel vm)
        {
            this.bl = bl;
            this.vm = vm;
            logger = DAL.Logging.LoggerFactory.GetLogger();

            EditTourCommand = new RelayCommand(_ => EditNewTour());
            CloseWindowCommand = new RelayCommand(_ => CloseWindow());
            if (tour == null)
            {
                logger.Error("Edit Tour: given tour is null");
                MessageBox.Show("no tour provided");
                CloseWindow();
            }
            else
            {
                currentTour = tour;
                Name = currentTour.Name;

                if (currentTour.Description == null)
                    Description = "";
                else
                    Description = currentTour.Description;
            }
        }

        public void EditNewTour()
        {
            if (currentTour != null)
            {
                currentTour.Name = Name;
                currentTour.Description = Description;
                int index = vm.Items.IndexOf(vm.Items.Where(X => X.Id == currentTour.Id).First());
                if (index > -1)
                {
                    vm.Items[index] = currentTour;
                    bl.UpdateTour(currentTour);

                    var res = ElasticSearchService.Instance.UpdateTour(currentTour.Id, Name, Description);
                    if (res == true)
                    {
                        MessageBox.Show("Update successful");
                        CloseWindow();

                    }
                    else
                    {
                        MessageBox.Show("Error updating tour!");
                    }

                }
                else
                {
                    logger.Error("Edit Tour: could not update data");
                    MessageBox.Show("Error while updating data");
                }
            }
            else
            {
                logger.Error("Edit Tour: unexpected error");
                MessageBox.Show("Error while updating data");
            }
        }

        public void CloseWindow()
        {
            DialogService.Instance.CloseDialog(this);
        }
    }
}
