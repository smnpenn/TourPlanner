using Newtonsoft.Json;

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

        public string? GetESUser()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/config.json";
            if (File.Exists(path))
            {
                var pConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(path));
                if (pConfig == null || pConfig["elasticuser"] == null)
                {
                    return null;
                }

                return pConfig["elasticuser"].ToString();
            }
            else
            {
                return null;
            }
        }
        public string? GetESPassword()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/config.json";
            if (File.Exists(path))
            {
                var pConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(path));
                if (pConfig == null || pConfig["elasticpassword"] == null)
                {
                    return null;
                }

                return pConfig["elasticpassword"].ToString();
            }
            else
            {
                return null;
            }
        }
        public string? GetESIndex()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/config.json";
            if (File.Exists(path))
            {
                var pConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(path));
                if (pConfig == null || pConfig["defaultindex"] == null)
                {
                    return null;
                }

                return pConfig["defaultindex"].ToString();
            }
            else
            {
                return null;
            }
        }
        public string? GetESFingerprint()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/config.json";
            if (File.Exists(path))
            {
                var pConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(path));
                if (pConfig == null || pConfig["fingerprint"] == null)
                {
                    return null;
                }

                return pConfig["fingerprint"].ToString();
            }
            else
            {
                return null;
            }
        }


    }
}
