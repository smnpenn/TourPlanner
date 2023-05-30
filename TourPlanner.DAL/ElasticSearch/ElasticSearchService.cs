using Elasticsearch.Net;
using Nest;
using System.Collections.ObjectModel;
using TourPlanner.Model;

namespace TourPlanner.DAL.ElasticSearch
{
    public class ElasticSearchService
    {
        private readonly ElasticClient _client;
        public ElasticSearchService()
        {
            var pool = new SingleNodeConnectionPool(new Uri("https://localhost:9200"));
            var settings = new ConnectionSettings(pool)
                .CertificateFingerprint("54:44:ec:01:47:37:9d:9f:58:dd:1a:be:54:bf:3b:4c:72:0e:a6:c7:19:67:60:ab:63:19:fc:0f:00:ee:53:ce")
                .BasicAuthentication("elastic", "elastic")
                .DefaultIndex("testindex-001")
                .DefaultMappingFor<ElasticTourDocument>(i => i.IndexName("tours-v1"))
                // .DefaultMappingFor<ElasticTourLogDocument>(i => i.IndexName("tourlogs-v1"))
                .EnableApiVersioningHeader();
            _client = new ElasticClient(settings);

        }


        public async Task<string> IndexTourDocument(int doc_id, string name, string description, string from, string to, int index, ObservableCollection<TourLog> logs)
        {

            var data = new ElasticTourDocument(doc_id, name, description, from, to, logs);

            var response = await _client.IndexAsync(
                data, idx => idx
                .Id(index)
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


        // Search 
        // TO-DO: Search in Logs?

        public void FuzzySearchLogs(string givenString)
        {

            var response = _client.Search<ElasticTourDocument>(s => s
            .Query(q => q
            .MultiMatch(c => c
            .Fields(f => f
                .Field(p => p.Name)
                .Field(p => p.From)
                .Field(p => p.To)
                .Field(p => p.Description)
             )
            .Query(givenString)
            .Fuzziness(Fuzziness.Auto)
            )));

            var docs = response.Documents;

            Console.WriteLine($"response:  {docs.Count}");

            foreach (var doc in docs)
            {
                Console.WriteLine($" Name: {doc.Name} Bio: {doc.Description} ");
            }
        }

        // Update documents
        public async Task<string> UpdateTourDocument(string id, int doc_id, string name, string description, string from, string to, ObservableCollection<TourLog> logs)
        {
            var updateRequest = new UpdateRequest<ElasticTourDocument, ElasticTourDocument>(id)
            {
                Doc = new ElasticTourDocument(doc_id, name, description, from, to, logs)

            };

            var updateResponse = await _client.UpdateAsync(updateRequest);

            if (updateResponse.IsValid)
            {
                return "Document successfully updated";
            }
            else
            {
                return "There was an error updating the document";
            }
        }

        // Delete Documents
        public string DeleteTourDocument(string id)
        {
            var deleteResponse = _client.Delete<ElasticTourDocument>(id);

            if (deleteResponse.IsValid)
            {
                return "Document deleted successfully";
            }
            else
            {
                return "Failed to delete the document";
            }
        }


        /**
         * 
         * 
         * 
         * 
         * Aufrufen von Sachen
         * var indexRequest = new IndexRequest<DOCUTYPE>
         * {
         * 
         * 
         * };
         * 
         * var indexResponse = ElasticSearchService.Index(indexRequest);
         * 
         * var deleteRequest = new DeleteRequest("my-index-001", id)
         * 
         * 
         * var deleteResponse = elasticSearchService.Delete<DOCUTYPE>(deleteRequest);
         * 
         * 
         * 
         * */
    }
}
