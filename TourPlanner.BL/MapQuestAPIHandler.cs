using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using System.Web;
using TourPlanner.Model;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Collections.Specialized;
using TourPlanner.DAL.Configuration;

namespace TourPlanner.BL
{
    public class MapQuestAPIHandler
    {
        public String APIKey = String.Empty;

        private HttpClient client = new HttpClient();
        private IConfigManager configManager = new ConfigManager();
        private DAL.Logging.ILoggerWrapper logger;

        public MapQuestAPIHandler()
        {
            APIKey = configManager.GetAPIKey();
            client.BaseAddress = new Uri("https://www.mapquestapi.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            logger = DAL.Logging.LoggerFactory.GetLogger();

            if(APIKey == null)
            {
                logger.Fatal("API Key invalid");
                throw new NullReferenceException("API Key invalid");
            }
        }

        public async Task<Tour> GetRoute(Tour tour)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["key"] = APIKey;
            query["from"] = tour.From;
            query["to"] = tour.To;
            
            if(tour.TransportType == TransportType.Car)
            {
                query["routeType"] = "fastest";
            }
            else if(tour.TransportType == TransportType.Bike)
            {
                query["routeType"] = "bicycle";
            }
            else if(tour.TransportType == TransportType.Pedestrian)
            {
                query["routeType"] = "pedestrian";
            }

            HttpResponseMessage response = await client.GetAsync("/directions/v2/route?" + query.ToString());
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(res);

                if (jsonResponse["route"]["routeError"] == null)
                {
                    double distance = Math.Round(jsonResponse["route"]["distance"].Value<double>(), 2);
                    double time = Math.Round(jsonResponse["route"]["realTime"].Value<double>()/60, 0);
                    string sessionId = jsonResponse["route"]["sessionId"].Value<string>();

                    tour.Distance = distance;
                    tour.EstimatedTime = (int)time;

                    //Get Map (sessionId)
                    tour.StaticMap = await GetStaticMap(sessionId);

                    if(tour.StaticMap == null)
                    {
                        return null;
                    }

                    return tour;
                }
            }

            return null;
        }

        public async Task<byte[]> GetStaticMap(string session) 
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["key"] = APIKey;
            query["size"] = "500,400";
            query["session"] = session;

            HttpResponseMessage response = await client.GetAsync("/staticmap/v5/map?" + query.ToString());

            if(response.IsSuccessStatusCode)
            {
                byte[] image = await response.Content.ReadAsByteArrayAsync();
                return image;
            }
            return null;
        }
    }
}
