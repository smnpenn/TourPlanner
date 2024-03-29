﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DAL
{
    public interface IDataManager
    {
        public void AddTour(Tour tour);
        public void DeleteTour(Tour tour);
        public void UpdateTour(Tour tour);
        public void AddTourLog(TourLog log);
        public void DeleteTourLog(TourLog log);
        public void UpdateTourLog(TourLog log);
        public ObservableCollection<Tour> GetTours();
        public ObservableCollection<TourLog> GetTourLogs(Tour tour);
    }
}
