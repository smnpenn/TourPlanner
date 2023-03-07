using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class TourLogs
    {
        public String Name { get; set; }
        public Tour RelatedTour { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public String Comment { get; set; } = String.Empty;
        public String Difficulty { get; set; } = "Easy"; //make enum?
        public int TotalTime { get; set; } //time in min
        public double Rating { get; set; } //0-5 stars
    }
}
