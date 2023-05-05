using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;

namespace TourPlanner.UI.ViewModels
{
    public class MoreOptionsViewModel
    {

        public ICommand ImportCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand SummaryReportCommand { get; }
        public ICommand SingleTourReportCommand { get; }
        private ITourPlannerManager bl;
        private TourSideListBarViewModel tourVM;
        private TourLogsSideListBarViewModel tourLogsVM;
        public MoreOptionsViewModel(ITourPlannerManager bl, TourSideListBarViewModel tourVM, TourLogsSideListBarViewModel tourLogsVM) 
        {
            this.tourVM = tourVM;
            this.tourLogsVM = tourLogsVM;
            this.bl = bl;
            ImportCommand = new RelayCommand(_ => Import());
            ExportCommand = new RelayCommand(_ => Export());
            SummaryReportCommand = new RelayCommand(_ => CreateSummaryReport());
            SingleTourReportCommand = new RelayCommand(_ => CreateSingleTourReport());
        }

        public void Import()
        {
            MessageBox.Show("Import");
        }

        public void Export()
        {
            bl.ExportData(tourVM.Items, "test123");
        }

        public void CreateSummaryReport()
        {
            MessageBox.Show("Summary report");
        }

        public void CreateSingleTourReport()
        {
            MessageBox.Show("Single tour report");
        }
    }
}
