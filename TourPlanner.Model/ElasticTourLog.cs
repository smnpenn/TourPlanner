namespace TourPlanner.Model
{
    public class ElasticTourLog
    {
        public int Id { get; private set; }
        public String Name { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
        public String? Comment { get; set; }
        public double Difficulty { get; set; } = 0; //make enum?
        public int TotalTime { get; set; } = 0; //time in min
        public double Rating { get; set; } = 0; //0-5 stars


        public ElasticTourLog(string name, DateTime dateTime, string? comment, double difficulty, int totalTime, double rating)
        {
            Name = name;
            DateTime = dateTime;
            Comment = comment;
            Difficulty = difficulty;
            TotalTime = totalTime;
            Rating = rating;
        }
    }
}
