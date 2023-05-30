using System.Collections.ObjectModel;

namespace TourPlanner.Model
{
    public class ElasticTourDocument
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String? Description { get; set; }
        public String From { get; set; }
        public String To { get; set; }

        public ObservableCollection<TourLog> Logs { get; set; }

        public ElasticTourDocument(int id, string name, string description, string from, string to, ObservableCollection<ElasticTourLog> logs)
        {
            Id = id;
            Name = name;
            Description = description;
            From = from;
            To = to;
            Logs = logs;
        }
    }
}
