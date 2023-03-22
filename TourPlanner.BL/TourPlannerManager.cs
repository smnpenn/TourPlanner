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
            throw new NotImplementedException();
        }

        public void AddTourLog(TourLog log)
        {
            throw new NotImplementedException();
        }

        public void DeleteTour(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void DeleteTourLog(TourLog log)
        {
            throw new NotImplementedException();
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
