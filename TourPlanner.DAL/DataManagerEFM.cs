using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DAL
{
    public class DataManagerEFM : IDataManager
    {
        private readonly TourplannerDBContext dbContext = new TourplannerDBContext();

        public DataManagerEFM()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        public void AddTour(Tour tour)
        {
            dbContext.Tours.Add(tour);
            dbContext.SaveChanges();
        }

        public void AddTourLog(TourLog log)
        {
            dbContext.Logs.Add(log);
            dbContext.SaveChanges();
        }

        public void DeleteTour(Tour tour)
        {
            dbContext.Tours.Remove(tour);
            dbContext.SaveChanges();
        }

        public void DeleteTourLog(TourLog log)
        {
            dbContext.Logs.Remove(log);
            dbContext.SaveChanges();
        }

        public ObservableCollection<TourLog> GetTourLogs(Tour tour)
        {
            ObservableCollection<TourLog> tourLogs = new ObservableCollection<TourLog>(dbContext.Logs.Where(X => X.RelatedTour.Id == tour.Id));
            return tourLogs;
        }

        public ObservableCollection<Tour> GetTours()
        {
            dbContext.Tours.Load();
            ObservableCollection<Tour> tours = new ObservableCollection<Tour>(dbContext.Tours);
            return tours;
        }

        public void UpdateTour(Tour tour)
        {
            Tour currentTour = dbContext.Tours.Where(X => X.Id == tour.Id).First();

            currentTour.Name = tour.Name;
            currentTour.From = tour.From;
            currentTour.To = tour.To;
            currentTour.Distance = tour.Distance;
            currentTour.Description = tour.Description;
            currentTour.EstimatedTime = tour.EstimatedTime;

            dbContext.SaveChanges();
        }

        public void UpdateTourLog(TourLog log)
        {
            TourLog currentLog = dbContext.Logs.Where(X => X.Id == log.Id).First();

            currentLog.Name = log.Name;
            currentLog.Comment = log.Comment;
            currentLog.DateTime = log.DateTime;
            currentLog.Rating = log.Rating;
            currentLog.RelatedTour = log.RelatedTour;
            currentLog.Difficulty = log.Difficulty;
            currentLog.TotalTime = log.TotalTime;

            dbContext.SaveChanges();
        }
    }
}
