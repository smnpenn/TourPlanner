using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public enum TransportType
    {
        Car = 0,
        Pedestrian = 1,
        Bike = 2
    }

    public class Tour
    {
        [Newtonsoft.Json.JsonIgnore]
        public int Id { get; private set; }
        public String Name { get; set; }
        public String? Description { get; set; }
        public String From { get; set; }
        public String To { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public double Distance { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int EstimatedTime { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public byte[] StaticMap { get; set; }
        //Transport type (enum)
        public TransportType TransportType { get; set; }

        [NotMapped]
        public ObservableCollection<TourLog> Logs { get; set; }
        
        public Tour()
        {

        }

        public Tour(string name, string description, string from, string to, TransportType transportType)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            Logs = new ObservableCollection<TourLog>();
            TransportType = transportType;
        }
    }
}
