using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL;
using TourPlanner.Model;

namespace TourPlanner.BL
{
    public class TourPlannerManagerImpl : ITourPlannerManager
    {
        
        private IDataManager dal;

        public TourPlannerManagerImpl(IDataManager dal)
        {
            this.dal = dal;
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
            throw new NotImplementedException();
        }

        public ObservableCollection<Tour> GetTours()
        {
            return dal.GetTours();
        }

        public void UpdateTour(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void UpdateTourLog(TourLog log)
        {
            throw new NotImplementedException();
        }
    }
}
