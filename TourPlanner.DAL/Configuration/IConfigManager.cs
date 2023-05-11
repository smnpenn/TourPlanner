using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL.Configuration
{
    public interface IConfigManager
    {
        public string? GetDBConfig();
    }
}
