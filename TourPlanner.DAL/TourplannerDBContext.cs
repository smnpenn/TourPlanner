using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DAL
{
    internal class TourplannerDBContext : DbContext
    {
        public DbSet<Tour> Tours{ get; set; }

        public DbSet<TourLog> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? config = GetDBConfig();

            if(config == null)
            {
                throw new IOException("Database config is missing or invalid.");
            }
            optionsBuilder.UseNpgsql(config);
        }

        private string? GetDBConfig()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/dbconfig.json";

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
    }
}
