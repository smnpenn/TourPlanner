using Nest;

namespace TourPlanner.DAL.ElasticSearch
{
    public class ElasticSearchService
    {
        private readonly ElasticClient _client;
        public ElasticSearchService()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("my-index-001");

            _client = new ElasticClient(settings);

        }


        public ISearchResponse<T> Search<T>(SearchDescriptor<T> searchDescriptor) where T : class
        {
            return _client.Search<T>(searchDescriptor);
        }

        public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector) where T : class
        {
            return _client.Search(selector);
        }

        // INDEXING
        public IndexResponse Index<T>(IIndexRequest<T> indexRequest) where T : class
        {
            return _client.Index(indexRequest);
        }

        // DELETE
        public DeleteResponse Delete<T>(IDeleteRequest deleteRequest) where T : class
        {
            return _client.Delete(deleteRequest);
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
