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

        public TourPlannerManager(IDataManager dal)
        {
            this.dal = dal;
            apiHandler = new MapQuestAPIHandler();
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
    }
}
