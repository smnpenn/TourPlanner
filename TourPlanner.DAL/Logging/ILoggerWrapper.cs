using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL.Logging
{
    public interface ILoggerWrapper
    {
        void Debug(string message);
        void Error(string message);
        void Info(string message);
        void Warn(string message);
        void Fatal(string message);
    }
}
