using Elasticsearch.Net;
using Nest;
using System.Collections.ObjectModel;
using TourPlanner.Model;

namespace TourPlanner.DAL.ElasticSearch
{
    public class ElasticSearchService
    {

        private static ElasticSearchService instance = null;

        public static ElasticSearchService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ElasticSearchService();
                }
                return instance;
            }
        }

        private readonly ElasticClient _client;
        public ElasticSearchService()
        {
            var pool = new SingleNodeConnectionPool(new Uri("https://localhost:9200"));
            var settings = new ConnectionSettings(pool)
                .CertificateFingerprint("54:44:ec:01:47:37:9d:9f:58:dd:1a:be:54:bf:3b:4c:72:0e:a6:c7:19:67:60:ab:63:19:fc:0f:00:ee:53:ce") // fuck this
                .BasicAuthentication("elastic", "elastic")
                .DefaultIndex("tours-v1")
                .DefaultMappingFor<ElasticTourDocument>(i => i.IndexName("tours-v1"))
                // .DefaultMappingFor<ElasticTourLogDocument>(i => i.IndexName("tourlogs-v1"))
                .EnableApiVersioningHeader();
            _client = new ElasticClient(settings);

        }

        // Check connection

        public bool CheckConnection()
        {
            var pingResponse = _client.Ping();

            if (pingResponse.IsValid)
            {
                Console.WriteLine("Successfully connected to Elasticsearch");
                return true;
            }
            else
            {
                Console.WriteLine("Failed to connect to Elasticsearch");
                return false;
            }
        }

        // Index new Tour
        public async Task<string> IndexTourDocument(Tour tour)
        {
            ObservableCollection<ElasticTourLog> logs = new ObservableCollection<ElasticTourLog>();

            var data = new ElasticTourDocument(tour.Id, tour.Name, tour.Description, tour.From, tour.To, logs);

            var response = await _client.IndexAsync(
                data, idx => idx
                .Id(tour.Id)
            );

            if (response.Result == Result.Created)
            {
                Console.WriteLine("Successfully created!");
                return response.ToString();
            }
            else
            {
                Console.WriteLine("Failed to create document!");
                return "failed";
            }
        }

        // Search Function

        public List<ElasticTourDocument> FuzzySearch(string givenString)
        {
            List<ElasticTourDocument> tourDocs = new List<ElasticTourDocument>();
            var response = _client.Search<ElasticTourDocument>(s => s
            .Query(q => q
                .Bool(b => b
                    .Should(s => s
                        .MultiMatch(x => x
                            .Fields(f => f
                                .Field(p => p.Name)
                                .Field(p => p.Description)
                                .Field(p => p.From)
                                .Field(p => p.To)
                            )
                            .Query(givenString)
                            .Fuzziness(Fuzziness.Auto)
                         ),
                         s => s
                         .Nested(n => n
                         .Path(p => p.Logs)
                         .Query(nq => nq
                         .MultiMatch(mm => mm
                         .Fields(f => f
                         .Field(p => p.Logs[0].Name)
                         .Field(p => p.Logs[0].Comment)
                         )
                         .Query(givenString)
                         .Fuzziness(Fuzziness.Auto)
            )))))));

            var docs = response.Documents;

            Console.WriteLine($"response:  {docs.Count}");

            foreach (var doc in docs)
            {
                tourDocs.Add(doc);
            }

            return tourDocs;
        }

        // Get TourDocument based on id
        public ElasticTourDocument GetElasticTourDocumentById(int id)
        {
            var response = _client.Get<ElasticTourDocument>(id, g => g.Index("tours-v1"));

            if (response.IsValid && response.Found)
            {
                return response.Source;
            }

            return null;
        }

        // add new tourlog to Tour
        public bool AddTourLog(ElasticTourDocument tour)
        {
            var updateResponse = _client.Update<ElasticTourDocument>(tour.Id, u => u
                .Doc(tour)
                .Refresh(Refresh.True)
            );
            if (updateResponse.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Update Tour
        public bool UpdateTour(int tourid, string newName, string newDescription)
        {
            ElasticTourDocument existingTour = GetElasticTourDocumentById(tourid);
            if (existingTour != null)
            {
                existingTour.Name = newName;
                existingTour.Description = newDescription;

                var updateResponse = _client.Update<ElasticTourDocument, object>(existingTour.Id, u => u
                    .Script(s => s
                        .Source("ctx._source.name = params.newName; ctx._source.description = params.newDescription")
                        .Params(p => p
                            .Add("newName", newName)
                            .Add("newDescription", newDescription)
                            )
                        )
                    .Refresh(Refresh.True));

                if (updateResponse.IsValid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Update Tour Logs
        public bool UpdateTourLog(int tourid, int logId, string newName, string newComment, double newRating, int newTotalTime, DateTime newDate, double newDifficulty)
        {
            ElasticTourDocument existingTour = GetElasticTourDocumentById(tourid);

            if (existingTour != null)
            {

                ElasticTourLog exisitingLog = existingTour.Logs.FirstOrDefault(log => log.Id == logId);
                if (exisitingLog != null)
                {
                    exisitingLog.Name = newName;
                    exisitingLog.Comment = newComment;
                    exisitingLog.Rating = newRating;
                    exisitingLog.DateTime = newDate;
                    exisitingLog.Difficulty = newDifficulty;
                    exisitingLog.TotalTime = newTotalTime;

                    var updateResponse = _client.Update<ElasticTourDocument>(existingTour.Id, u => u
                        .Doc(existingTour)
                        .Refresh(Refresh.True));

                    if (updateResponse.IsValid)
                    {
                        Console.WriteLine("Updating tour was successful!");
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public bool DeleteLogById(int tourId, int logId)
        {
            ElasticTourDocument tourDocument = ElasticSearchService.Instance.GetElasticTourDocumentById(tourId);
            if (tourDocument == null)
            {
                return false;
            }

            int logIndex = -1;
            for (int i = 0; i < tourDocument.Logs.Count; i++)
            {
                if (tourDocument.Logs[i].Id == logId)
                {
                    logIndex = i;
                    break;
                }
            }

            if (logIndex == -1)
            {
                // Log with given ID not found
                return false;
            }


            tourDocument.Logs.RemoveAt(logIndex);

            var updateResponse = _client.Update<ElasticTourDocument>(tourDocument.Id, u => u
                        .Doc(tourDocument)
                        .Refresh(Refresh.True));

            if (updateResponse.IsValid)
            {
                Console.WriteLine("Updating tour was successful!");
                return true;
            }
            else
            {
                return false;
            }

        }



        // Delete Documents
        public bool DeleteTourDocument(string id)
        {
            var deleteResponse = _client.Delete<ElasticTourDocument>(id);

            if (deleteResponse.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
