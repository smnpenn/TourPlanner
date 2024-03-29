﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.UI.Service;
using TourPlanner.Model;

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

        private static TourPlanner.DAL.Logging.ILoggerWrapper logger;
        public MoreOptionsViewModel(ITourPlannerManager bl, TourSideListBarViewModel tourVM, TourLogsSideListBarViewModel tourLogsVM) 
        {
            this.tourVM = tourVM;
            this.tourLogsVM = tourLogsVM;
            this.bl = bl;
            logger = TourPlanner.DAL.Logging.LoggerFactory.GetLogger();
            ImportCommand = new RelayCommand(_ => Import());
            ExportCommand = new RelayCommand(_ => Export());
            SummaryReportCommand = new RelayCommand(_ => CreateSummaryReport());
            SingleTourReportCommand = new RelayCommand(_ => CreateSingleTourReport());
        }

        public async void Import()
        {
            Stream? fileStream = DialogService.Instance.ShowOpenFileDialog("json");

            if(fileStream != null)
            {
                try
                {
                    List<Tour> addedData = await bl.ImportData(fileStream);
                    
                    if(addedData != null)
                    {
                        foreach (Tour t in addedData)
                        {
                            tourVM.Items.Add(t);
                        }
                    }

                    MessageBox.Show("Import successful");
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("File not found.");
                    logger.Error("Import: file not found");
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not import data from given file.");
                    logger.Error("Import: could not import data");
                }
            }
        }

        public void Export()
        {
            string? filename = DialogService.Instance.ShowSaveFileDialog("Tourplanner_export", "json");

            if (filename != null)
            {
                try
                {
                    bl.ExportData(tourVM.Items, filename);
                    MessageBox.Show("Export successful");
                }
                catch(Exception)
                {
                    MessageBox.Show("Error while exporting data!");
                    logger.Error("Export: could not export data");
                }
            }
        }

        public void CreateSummaryReport()
        {
            string? filename = DialogService.Instance.ShowSaveFileDialog($"SummaryReport", "pdf");

            // Process save file dialog box results
            if (filename != null)
            {
                bl.GenerateSummaryReport(tourVM.Items, filename);
                DialogService.Instance.OpenFileExplorer(filename);
            }
            else
            {
                logger.Error("Summary report: no file declared");
            }
        }

        public void CreateSingleTourReport()
        {
            if (tourVM.SelectedItem == null)
            {
                logger.Info("Single-tour report: No tour selected");
                MessageBox.Show("Please select a tour first.");
                return;
            }
            string? filename = DialogService.Instance.ShowSaveFileDialog($"Tour{tourVM.SelectedItem.Id}_Report", "pdf");

            // Process save file dialog box results
            if (filename != null)
            {
                bl.GenerateTourReport(tourVM.SelectedItem, filename);
                DialogService.Instance.OpenFileExplorer(filename);
            }
            else
            {
                logger.Error("Single-tour report: no file declared");
            }
        }
    }
}
