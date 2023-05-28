using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL;
using TourPlanner.DAL;
using TourPlanner.DAL.Logging;

namespace TourPlanner.UnitTests
{
    public class ProjectSetupTests
    {
        private IDataManager dataManager;
        [Test]
        public void ProjectSetup_DatabaseConnection()
        {
            dataManager = new DataManagerEFM(true);
        }

        [Test]
        public void ProjectSetup_Log4NetConfig()
        {
            ILoggerWrapper logger = LoggerFactory.GetLogger();
        }

        [Test]
        public async Task ProjectSetup_APIConnection()
        {
            MapQuestAPIHandler apiHandler = new MapQuestAPIHandler();
            Model.Tour tour = await apiHandler.GetRoute(new Model.Tour("Name", "Test", "Brixen", "Vahrn", Model.TransportType.Car));

            Assert.NotNull(tour);
        }
    
    }
}

