using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL;
using TourPlanner.DAL;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UnitTests.ViewModels
{
    public class MainViewModelTests
    {
        private MainViewModel mVM;
        private TourSideListBarViewModel tourVM;
        private TourLogsSideListBarViewModel logVM;
        private ITourPlannerManager bl;
        private IDataManager dataManager;

        [SetUp]
        public void Setup()
        {
            dataManager = new DataManagerEFM();
            bl = new TourPlannerManager(dataManager);
            tourVM = new TourSideListBarViewModel(bl);
            logVM = new TourLogsSideListBarViewModel(bl);
            mVM = new MainViewModel(bl, logVM, tourVM);
        }

        [Test]
        public void MainViewModel_AddTour()
        {

        }
    }
}
