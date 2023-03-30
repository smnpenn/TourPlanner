using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DAL
{
    public class DataManagerMemoryImpl : IDataManager
    {

        //private Tour tour1 = new Tour("Tour 1", "Test 1", "Wien", "Salzburg", 100, 100, new ObservableCollection<TourLog>());
        //private Tour tour2 = new Tour("Tour 2", "Test 2", "Brixen", "Raas", 200, 200, new ObservableCollection<TourLog>());

        private ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();

        public DataManagerMemoryImpl() 
        {
            /*TourLog log11 = new TourLog("Tour1Log1", tour1);
            TourLog log12 = new TourLog("Tour1Log2", tour1);
            TourLog log21 = new TourLog("Tour2Log1", tour2);
            TourLog log22 = new TourLog("Tour2Log2", tour2);

            tour1.TourLogs.Add(log11);
            tour1.TourLogs.Add(log12);
            tour2.TourLogs.Add(log21);
            tour2.TourLogs.Add(log22);

            _tours.Add(tour1);
            _tours.Add(tour2);*/
        }

        public void AddTour(Tour tour)
        {
            _tours.Add(tour);
        }

        public void AddTourLog(TourLog log)
        {
            if (_tours.Contains(log.RelatedTour))
            {
                //_tours.Where(X=> X == log.RelatedTour).First().TourLogs.Add(log);
            }
        }

        public void DeleteTour(Tour tour)
        {
            _tours.Remove(tour);
        }

        public void DeleteTourLog(TourLog log)
        {
            if (_tours.Contains(log.RelatedTour))
            {
                //_tours.Where(X => X == log.RelatedTour).First().TourLogs.Remove(log);
            }
        }

        public ObservableCollection<TourLog> GetTourLogs(Tour tour)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Tour> GetTours()
        {
            return _tours;
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
