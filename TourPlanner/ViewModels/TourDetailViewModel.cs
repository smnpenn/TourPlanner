using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.UI.ViewModels
{
    public class TourDetailViewModel
    {
        private Tour tour;

        public String Name { get; set; }
        public String Description { get; set; }
        public String FromToText { get; set; }
        public String DistanceText { get; set; }
        public String EstimatedTimeText { get; set; }
        public String TransportType { get; set; }

        public TourDetailViewModel(Tour tour) 
        { 
            this.tour = tour;
            Name = tour.Name;
            Description = tour.Description;
            FromToText = $"{tour.From} --> {tour.To}";
            DistanceText = $"{tour.Distance} km";
            EstimatedTimeText = $"{tour.EstimatedTime} min";
            TransportType = "Transport Type";
        }
    }
}
