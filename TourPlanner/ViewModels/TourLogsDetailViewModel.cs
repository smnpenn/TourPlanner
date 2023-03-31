using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.UI.ViewModels
{
    public class TourLogsDetailViewModel
    {
        public ObservableCollection<TourLog> Logs { get; set; }
        public TourLogsDetailViewModel(ObservableCollection<TourLog> tourLogs) 
        {
            Logs = tourLogs;
        }
    }
}
