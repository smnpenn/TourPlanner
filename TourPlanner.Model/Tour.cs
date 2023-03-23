using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class Tour
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public double Distance { get; set; }
        public int EstimatedTime { get; set; }
        //Tour information (API Map)
        //Transport type (enum)
        public ObservableCollection<TourLog> TourLogs { get; set; } = new ObservableCollection<TourLog>();

        public Tour(string name, string description, string from, string to, double distance, int estimatedTime, ObservableCollection<TourLog> tourLogs)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            Distance = distance;
            EstimatedTime = estimatedTime;
            TourLogs = tourLogs;
        }
    }
}
