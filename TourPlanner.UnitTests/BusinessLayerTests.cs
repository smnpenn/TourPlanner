using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL;
using TourPlanner.DAL;
using TourPlanner.UI.ViewModels;
using TourPlanner.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;

namespace TourPlanner.UnitTests
{
    public class BusinessLayerTests
    {
        private ITourPlannerManager bl;
        private IDataManager dataManager;

        [SetUp]
        public void Setup()
        {
            dataManager = new DataManagerEFM(true);
            bl = new TourPlannerManager(dataManager);
        }

        [Test]
        public async Task BusinessLayer_CreateValidRoute()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);

            //Act
            tour = await bl.GetRoute(tour);
            Assert.NotNull(tour);
        }

        [Test]
        public async Task BusinessLayer_CreateInvalidRoute()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "21323", "ff232", TransportType.Car);

            //Act
            tour = await bl.GetRoute(tour);
            Assert.Null(tour);
        }

        [Test]
        public async Task BusinessLayer_AddValidTour()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);

            //Act
            bl.AddTour(tour);
            Assert.That(bl.GetTours().Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task BusinessLayer_AddInvalidTour()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "21323", "ff232", TransportType.Car);
            tour = await bl.GetRoute(tour);

            //Act
            bl.AddTour(tour);
            Assert.That(bl.GetTours().Count, Is.EqualTo(0));
        }

        [Test]
        public async Task BusinessLayer_DeleteTour()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);
            bl.AddTour(tour);

            //Act
            bl.DeleteTour(tour);
            Assert.That(bl.GetTours().Count, Is.EqualTo(0));
        }

        [Test]
        public async Task BusinessLayer_UpdateTour()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);
            bl.AddTour(tour);
            tour.Name = "Name2";
            tour.Description = "Description2";

            //Act
            bl.UpdateTour(tour);
            Tour updatedTour = bl.GetTours().Where(X => X.Id == tour.Id).First();
            Assert.That(updatedTour.Name, Is.EqualTo("Name2"));
            Assert.That(updatedTour.Description, Is.EqualTo("Description2"));

        }

        [Test]
        public async Task BusinessLayer_GetTours()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);
            bl.AddTour(tour);

            //Act
            ObservableCollection<Tour> tours = bl.GetTours();
            Assert.That(tours.Count, Is.EqualTo(1));
            Assert.That(tours[0], Is.EqualTo(tour));
        }

        [Test]
        public async Task BusinessLayer_AddLog()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);
            bl.AddTour(tour);

            //Act
            TourLog log = new TourLog("Log", tour, DateTime.Now, "Comment", 3.0, 120, 4);
            bl.AddTourLog(log);
            Assert.That(bl.GetTourLogs(tour).Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task BusinessLayer_AddLog_NonExistingTour()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);

            //Act
            TourLog log = new TourLog("Log", tour, DateTime.Now, "Comment", 3.0, 120, 4);
            bl.AddTourLog(log);
            Assert.That(bl.GetTourLogs(tour).Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task BusinessLayer_DeleteLog()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);
            bl.AddTour(tour);
            TourLog log = new TourLog("Log", tour, DateTime.Now, "Comment", 3.0, 120, 4);
            bl.AddTourLog(log);

            //Act
            bl.DeleteTourLog(log);
            Assert.That(bl.GetTourLogs(tour).Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task BusinessLayer_UpdateLog()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);
            bl.AddTour(tour);
            TourLog log = new TourLog("Log", tour, DateTime.Now, "Comment", 3.0, 120, 4);
            bl.AddTourLog(log);
            log.Name = "NewLog";
            log.TotalTime = 100;

            //Act
            bl.UpdateTourLog(log);
            TourLog updatedLog = bl.GetTourLogs(tour).Where(X => X.Id == log.Id).First();
            Assert.That(updatedLog.Name, Is.EqualTo("NewLog"));
            Assert.That(updatedLog.TotalTime, Is.EqualTo(100));
        }

        [Test]
        public async Task BusinessLayer_GetLogs()
        {
            //Arrange
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);
            bl.AddTour(tour);
            TourLog log = new TourLog("Log", tour, DateTime.Now, "Comment", 3.0, 120, 4);
            bl.AddTourLog(log);

            //Act
            ObservableCollection<TourLog> logs = bl.GetTourLogs(tour);
            Assert.That(logs.Count, Is.EqualTo(1));
            Assert.That(logs[0], Is.EqualTo(log));
        }

        [Test]
        public async Task BusinessLayer_Export()
        {
            //Arrange
            string path = AppDomain.CurrentDomain.BaseDirectory + "/exportTest.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);
            TourLog log = new TourLog("Log", tour, DateTime.Now, "Comment", 3.0, 120, 4);
            tour.Logs.Add(log);
            bl.AddTour(tour);

            //Act
            bl.ExportData(bl.GetTours(), path);
            Assert.That(File.Exists(path));
        }

        [Test]
        public async Task BusinessLayer_ImportValid()
        {
            //Arrange
            string json = "[{\"Name\":\"Tour2\",\"Description\":\"sdw\",\"From\":\"Bozen\",\"To\":\"Brixen\",\"Logs\":[{\"Name\":\"Log\",\"DateTime\":\"2023-04-20T10:25:01.789898\",\"Comment\":\"wdwd\",\"Difficulty\":3.0,\"TotalTime\":120,\"Rating\":3.0},{\"Name\":\"Log\",\"DateTime\":\"2023-04-20T11:04:24.891694\",\"Comment\":\"dwwd\",\"Difficulty\":2.0,\"TotalTime\":2,\"Rating\":2.0}]}]";
            string path = AppDomain.CurrentDomain.BaseDirectory + "/importTest_valid.json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(json);
            }

            //Act
            await bl.ImportData(File.Open(path, FileMode.Open));
            Tour tour = bl.GetTours()[0];
            Assert.That(bl.GetTours().Count(), Is.EqualTo(1));
            Assert.That(bl.GetTourLogs(tour).Count(), Is.EqualTo(2));
            Assert.That(tour.Name, Is.EqualTo("Tour2"));
        }

        [Test]
        public void BusinessLayer_Import_Invalid()
        {
            //Arrange
            string json = "[{\"what\": \"invalid\"}]";
            string path = AppDomain.CurrentDomain.BaseDirectory + "/importTest_invalid.json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(json);
            }

            //Act
            Assert.That(async () => await bl.ImportData(File.Open(path, FileMode.Open)), Throws.Exception.TypeOf<InvalidDataException>());
        }

        [Test]
        public async Task BusinessLayer_Import_NoLogs_ListGetsInitialized()
        {
            //Arrange
            string json = "[{\"Name\":\"Tour2\",\"Description\":\"sdw\",\"From\":\"Bozen\",\"To\":\"Brixen\",\"Logs\":null}]";
            string path = AppDomain.CurrentDomain.BaseDirectory + "/importTest_valid_nologs.json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(json);
            }

            //Act
            await bl.ImportData(File.Open(path, FileMode.Open));
            Assert.NotNull(bl.GetTours()[0].Logs);
        }

        [Test]
        public async Task BusinessLayer_TourReport_FileGetsCreated()
        {
            //Arrange
            string path = AppDomain.CurrentDomain.BaseDirectory + "/TourReportTest.pdf";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);
            TourLog log = new TourLog("Log", tour, DateTime.Now, "Comment", 3.0, 120, 4);
            tour.Logs.Add(log);
            bl.AddTour(tour);

            //Act
            bl.GenerateTourReport(bl.GetTours()[0], path);

            Assert.That(File.Exists(path));
        }

        [Test]
        public async Task BusinessLayer_SummaryReport_FileGetsCreated()
        {
            //Arrange
            string path = AppDomain.CurrentDomain.BaseDirectory + "/SummaryReportTest.pdf";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            Tour tour = new Tour("Name", "Description", "Wien", "Salzburg", TransportType.Car);
            tour = await bl.GetRoute(tour);
            TourLog log = new TourLog("Log", tour, DateTime.Now, "Comment", 3.0, 120, 4);
            tour.Logs.Add(log);
            bl.AddTour(tour);

            //Act
            bl.GenerateSummaryReport(bl.GetTours(), path);

            Assert.That(File.Exists(path));
        }

    }
}
