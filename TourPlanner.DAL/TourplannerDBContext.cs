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
            Configuration.IConfigManager configManager = new Configuration.ConfigManager();
            string? config = configManager.GetDBConfig();

            if(config == null)
            {
                throw new IOException("Database config is missing or invalid.");
            }
            optionsBuilder.UseNpgsql(config);
        }
    }
}
