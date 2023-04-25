﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class Tour
    {
        [Newtonsoft.Json.JsonIgnore]
        public int Id { get; private set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String From { get; set; }
        public String To { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public double Distance { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int EstimatedTime { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public byte[] StaticMap { get; set; }
        //Tour information (API Map)
        //Transport type (enum)
        [NotMapped]
        public ObservableCollection<TourLog> Logs { get; set; }
        
        public Tour()
        {

        }

        public Tour(string name, string description, string from, string to)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
        }
    }
}
