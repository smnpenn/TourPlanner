using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL.Configuration
{
    public class ConfigManager : IConfigManager
    {
        public string? GetDBConfig()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/config.json";

            if (File.Exists(path))
            {
                var pConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(path));

                if (pConfig == null || pConfig["host"] == null || pConfig["username"] == null || pConfig["password"] == null || pConfig["database"] == null)
                {
                    return null;
                }

                return $"Include Error Detail=True;Host={pConfig["host"]};Username={pConfig["username"]};Password={pConfig["password"]};Database={pConfig["database"]}";
            }
            return null;
        }

        public string? GetAPIKey()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/config.json";

            if (File.Exists(path))
            {
                var pConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(path));

                if (pConfig == null || pConfig["apikey"] == null)
                {
                    return null;
                }

                return pConfig["apikey"].ToString();
            }
            return null;
        }
    }
}
