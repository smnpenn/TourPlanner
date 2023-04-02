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

namespace TourPlanner.BL
{
    public class MapQuestAPIHandler
    {
        public String APIKey = "Ow4N92lkSVhzev2Mp2Km2HIcR5jysxP0";

        private HttpClient client = new HttpClient();

        public MapQuestAPIHandler()
        {
            client.BaseAddress = new Uri("https://www.mapquestapi.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Tour> GetRoute(Tour tour)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["key"] = APIKey;
            query["from"] = tour.From;
            query["to"] = tour.To;
            query["routeType"] = "car";

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
