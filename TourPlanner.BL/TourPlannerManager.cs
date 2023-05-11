using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            dal.AddTour(tour);
        }

        public void AddTourLog(TourLog log)
        {
            dal.AddTourLog(log);
        }

        public void DeleteTour(Tour tour)
        {
            dal.DeleteTour(tour);
        }

        public void DeleteTourLog(TourLog log)
        {
            dal.DeleteTourLog(log);
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
            dal.UpdateTour(tour);
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
    }
}
