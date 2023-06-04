using System.Collections.ObjectModel;
using TourPlanner.DAL;
using TourPlanner.DAL.ElasticSearch;
using TourPlanner.Model;

namespace TourPlanner.BL
{
    public class TourPlannerManager : ITourPlannerManager
    {

        private IDataManager dal;
        private MapQuestAPIHandler apiHandler;
        private DataImporter dataImporter;
        private DataExporter dataExporter;
        private ReportGenerator reportGenerator;
        private static TourPlanner.DAL.Logging.ILoggerWrapper logger;

        public TourPlannerManager(IDataManager dal)
        {
            this.dal = dal;
            apiHandler = new MapQuestAPIHandler();
            dataImporter = new DataImporter();
            dataExporter = new DataExporter();
            reportGenerator = new ReportGenerator();
            logger = TourPlanner.DAL.Logging.LoggerFactory.GetLogger();
        }

        public void AddTour(Tour tour)
        {
            if (tour != null)
            {
                dal.AddTour(tour);
                if (ElasticSearchService.Instance.CheckConnection() == true)
                {
                    var res = ElasticSearchService.Instance.IndexTourDocument(tour);
                    if (res != null)
                    {
                        Console.WriteLine("Adding tour to ES was successful");
                    }
                    else
                    {
                        Console.WriteLine("Error when adding tour to ES");

                    }
                }
                else
                {
                    Console.WriteLine("Error when connecting to ES");
                }
            }
        }

        public void AddTourLog(TourLog log)
        {
            if (log != null)
            {
                if (log.RelatedTour != null)
                {
                    if (GetTours().Contains(log.RelatedTour))
                    {
                        dal.AddTourLog(log);
                        if (ElasticSearchService.Instance.CheckConnection() == true)
                        {
                            var doc = ElasticSearchService.Instance.GetElasticTourDocumentById(log.RelatedTour.Id);
                            if (doc != null)
                            {
                                doc.Logs.Add(new ElasticTourLog(log.Id, log.Name, log.DateTime, log.Comment, log.Difficulty, log.TotalTime, log.Rating));

                                var res = ElasticSearchService.Instance.AddTourLog(doc);
                                if (res == true)
                                {
                                    Console.WriteLine("Adding TourLog to ES was successful!");
                                }
                                else
                                {
                                    Console.WriteLine("Error when adding TourLog to ES");
                                }

                            }
                            else
                            {
                                Console.WriteLine("Error fetching associated Tour in ES");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error when connecting to ES");

                        }
                    }
                }
            }
        }

        public void DeleteTour(Tour tour)
        {
            if (tour != null)
            {
                dal.DeleteTour(tour);
                if (ElasticSearchService.Instance.CheckConnection() == true)
                {
                    var res = ElasticSearchService.Instance.DeleteTourDocument(tour.Id.ToString());
                    if (res == true)
                    {
                        Console.WriteLine("Removing Tour from ES was successful!");
                    }
                    else
                    {
                        Console.WriteLine("There was an Error when Removing Tour from ES");

                    }
                }
                else
                {
                    Console.WriteLine("Error when connecting to ES");
                }
            }
        }

        public void DeleteTourLog(TourLog log)
        {
            if (log != null)
            {
                dal.DeleteTourLog(log);
                if (ElasticSearchService.Instance.CheckConnection() == true)
                {
                    ElasticTourDocument doc = ElasticSearchService.Instance.GetElasticTourDocumentById(log.RelatedTour.Id);
                    if (doc != null)
                    {
                        var res = ElasticSearchService.Instance.DeleteLogById(doc.Id, log.Id);
                        if (res == true)
                        {
                            Console.WriteLine("Removing TourLog from ES was successful");
                        }
                        else
                        {
                            Console.WriteLine("There was a problem when removing TourLog from ES");

                        }
                    }
                    else
                    {
                        Console.WriteLine("Error fetching associated Tour in ES");
                    }
                }
                else
                {
                    Console.WriteLine("Error when connecting to ES");
                }
            }
        }

        public ObservableCollection<TourLog> GetTourLogs(Tour tour)
        {
            return dal.GetTourLogs(tour);
        }

        public ObservableCollection<Tour> GetTours()
        {
            return dal.GetTours();
        }

        public void UpdateTour(Tour tour)
        {
            if (tour != null)
            {
                dal.UpdateTour(tour);
                if (ElasticSearchService.Instance.CheckConnection() == true)
                {
                    var res = ElasticSearchService.Instance.UpdateTour(tour.Id, tour.Name, tour.Description);
                    if (res == true)
                    {
                        Console.WriteLine("Tour has been updated successfully in ES");

                    }
                    else
                    {
                        Console.WriteLine("There has been a error when updating the tour in ES");

                    }
                }
                else
                {
                    Console.WriteLine("Error when connecting to ES");
                }

            }
        }

        public void UpdateTourLog(TourLog log)
        {
            dal.UpdateTourLog(log);
            if (ElasticSearchService.Instance.CheckConnection() == true)
            {
                var res = ElasticSearchService.Instance.UpdateTourLog(log.RelatedTour.Id, log.Id, log.Name, log.Comment, log.Rating, log.TotalTime, log.DateTime, log.Difficulty);
                if (res == true)
                {
                    Console.WriteLine("TourLog has been updated successfully in ES");
                }
                else
                {
                    Console.WriteLine("There has been a error when updating the TourLog in ES");
                }
            }
            else
            {
                Console.WriteLine("Error when connecting to ES");
            }
        }

        public async Task<Tour> GetRoute(Tour tour)
        {
            return await apiHandler.GetRoute(tour);
        }

        public void ExportData(ObservableCollection<Tour> tours, string path)
        {
            dataExporter.ExportData(tours, path);
        }

        public async Task<List<Tour>> ImportData(Stream fileStream)
        {
            List<Tour> data = dataImporter.ImportData(fileStream);

            if (data != null)
            {
                foreach (Tour t in data)
                {
                    await apiHandler.GetRoute(t);
                    dal.AddTour(t);
                    if (t.Logs != null)
                    {
                        foreach (TourLog log in t.Logs)
                        {
                            log.RelatedTour = t;
                            dal.AddTourLog(log);
                        }

                    }
                    else
                    {
                        t.Logs = new ObservableCollection<TourLog>();
                    }
                    CalculateAdditionalAttributes(t);
                }
            }

            return data;
        }

        public void GenerateTourReport(Tour tour, string path)
        {
            reportGenerator.GenerateTourReport(tour, path);
        }

        public void GenerateSummaryReport(ObservableCollection<Tour> tours, string path)
        {
            reportGenerator.GenerateSummaryReport(tours, path);
        }

        public void CalculateAdditionalAttributes(Tour tour)
        {
            if (tour != null)
            {
                ObservableCollection<TourLog> logs = GetTourLogs(tour);
                if (logs != null)
                {
                    tour.Popularity = GetPopularity(logs);
                    tour.ChildFriendliness = GetChildFriendliness(logs);
                }
                else
                {
                    tour.Popularity = 0;
                    tour.ChildFriendliness = 0;
                }
            }

        }

        private double GetChildFriendliness(ObservableCollection<TourLog> logs)
        {
            if (logs.Count == 0)
            {
                return 0;
            }
            double avgDifficulty = 0;
            double avgTotalTime = 0;
            foreach (TourLog log in logs)
            {
                avgDifficulty += log.Difficulty;
                avgTotalTime += log.TotalTime;
            }
            avgDifficulty /= logs.Count;
            avgTotalTime /= logs.Count;

            if (avgDifficulty <= 1.5 && avgTotalTime <= 60)
            {
                return 5;
            }
            else if (avgDifficulty <= 1.5 && avgTotalTime > 60)
            {
                return 4;
            }
            else if (avgDifficulty <= 2.5 && avgTotalTime <= 60)
            {
                return 4;
            }
            else if (avgDifficulty <= 2.5 && avgTotalTime > 60)
            {
                return 3;
            }
            else if (avgDifficulty <= 3.5 && avgTotalTime <= 60)
            {
                return 2;
            }
            else if (avgDifficulty <= 3.5 && avgTotalTime > 60)
            {
                return 2;
            }
            else if (avgDifficulty <= 4.5 && avgTotalTime <= 60)
            {
                return 1;
            }
            else if (avgDifficulty <= 4.5 && avgTotalTime > 60)
            {
                return 0;
            }
            else if (avgDifficulty > 4.5)
            {
                return 0;
            }

            return -1;
        }

        private int GetPopularity(ObservableCollection<TourLog> logs)
        {
            if (logs.Count <= 0)
            {
                return 0;
            }
            else if (logs.Count <= 1)
            {
                return 1;
            }
            else if (logs.Count <= 3)
            {
                return 2;
            }
            else if (logs.Count <= 5)
            {
                return 3;
            }
            else if (logs.Count <= 10)
            {
                return 4;
            }
            else if (logs.Count > 10)
            {
                return 5;
            }
            return 0;
        }
    }
}
