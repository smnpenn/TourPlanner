using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BL
{
    public interface ITourPlannerManager
    {
        //defines all public methods in the business logic
        //mostly MapQuest API stuff and DAL communication

        public void AddTour(Tour tour);
        public void DeleteTour(Tour tour);
        public void UpdateTour(Tour tour);
        public void AddTourLog(TourLog log);
        public void DeleteTourLog(TourLog log);
        public void UpdateTourLog(TourLog log);
        public ObservableCollection<Tour> GetTours();
        public ObservableCollection<TourLog> GetTourLogs(Tour tour);

        public Task<Tour> GetRoute(Tour tour);
        public void ExportData(ObservableCollection<Tour> tours, string path);
        public Task<List<Tour>> ImportData(Stream fileStream);
        public void GenerateTourReport(Tour tour, string path);
        public void GenerateSummaryReport(ObservableCollection<Tour> tours, string path);
        public void CalculateAdditionalAttributes(Tour tour);
    }
}
