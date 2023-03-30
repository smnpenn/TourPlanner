using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            optionsBuilder.UseNpgsql("Include Error Detail=True;Host=localhost;Database=tourplanner;Username=postgres;Password=admin");
        }

    }
}
