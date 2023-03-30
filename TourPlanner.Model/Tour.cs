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
        public int Id { get; private set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public double Distance { get; set; }
        public int EstimatedTime { get; set; }
        //Tour information (API Map)
        //Transport type (enum)
        
        public Tour()
        {

        }

        public Tour(string name, string description, string from, string to, double distance, int estimatedTime)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            Distance = distance;
            EstimatedTime = estimatedTime;
        }
    }
}
