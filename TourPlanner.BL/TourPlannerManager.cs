using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.ObjectModel;
using TourPlanner.DAL;
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
            if(tour != null)
            {
                dal.AddTour(tour);
            }
        }

        public void AddTourLog(TourLog log)
        {
            if(log != null)
            {
                if(log.RelatedTour != null)
                {
                    if (GetTours().Contains(log.RelatedTour))
                    {
                        dal.AddTourLog(log);
                    }
                }
            }
        }

        public void DeleteTour(Tour tour)
        {
            if(tour != null)
            {
                dal.DeleteTour(tour);
            }
        }

        public void DeleteTourLog(TourLog log)
        {
            if(log != null)
            {
                dal.DeleteTourLog(log);
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
            if(tour != null) 
            {
                dal.UpdateTour(tour);
            }
        }

        public void UpdateTourLog(TourLog log)
        {
            dal.UpdateTourLog(log);
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

            if(data != null)
            {
                foreach(Tour t in data)
                {
                    await apiHandler.GetRoute(t);
                    dal.AddTour(t);
                    if(t.Logs != null)
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
                if(logs != null)
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
            if(logs.Count == 0)
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

            if(avgDifficulty <= 1.5 && avgTotalTime <= 60)
            {
                return 5;
            }
            else if(avgDifficulty <= 1.5 && avgTotalTime > 60)
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
            if(logs.Count <= 0)
            {
                return 0;
            }
            else if(logs.Count <= 1)
            {
                return 1;
            }
            else if(logs.Count <= 3)
            {
                return 2;
            }
            else if(logs.Count <= 5)
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
