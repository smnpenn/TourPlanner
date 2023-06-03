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
        public String FromText { get; set; }
        public String ToText { get; set; }
        public String DistanceText { get; set; }
        public String EstimatedTimeText { get; set; }
        public TransportType TransportType { get; set; }

        public String PopularityText { get; set; }
        public String ChildFriendlinessText { get; set; }

        public TourDetailViewModel(Tour tour) 
        { 
            this.tour = tour;
            Name = tour.Name;
            Description = tour.Description;
            FromText = tour.From;
            ToText = tour.To;
            DistanceText = $"{tour.Distance} km";
            EstimatedTimeText = $"{tour.EstimatedTime} min";
            TransportType = tour.TransportType;
            PopularityText = $"{tour.Popularity}";
            ChildFriendlinessText = $"{tour.ChildFriendliness}";
        }
    }
}
