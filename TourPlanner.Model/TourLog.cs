using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class TourLog
    {
        [Newtonsoft.Json.JsonIgnore]
        public int Id { get; private set; }
        public String Name { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Tour RelatedTour { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public String? Comment { get; set; }
        public double Difficulty { get; set; } = 0; //make enum?
        public int TotalTime { get; set; } = 0; //time in min
        public double Rating { get; set; } = 0; //0-5 stars


        public TourLog() { }
        public TourLog(string name, Tour relatedTour, DateTime dateTime, string? comment, double difficulty, int totalTime, double rating)
        {
            Name = name;
            RelatedTour = relatedTour;
            DateTime = dateTime;
            Comment = comment;
            Difficulty = difficulty;
            TotalTime = totalTime;
            Rating = rating;
        }

        public TourLog(string name, Tour relatedTour)
        {
            Name = name;
            RelatedTour = relatedTour;
        }
    }
}
