using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BL
{
    public class ReportGenerator
    {
        public ReportGenerator() { }

        public void GenerateTourReport(Tour tour, string path)
        {
            var writer = new PdfWriter(path);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument);

            Paragraph title = new Paragraph(tour.Name).SetFontSize(28).SetBold().SetTextAlignment(TextAlignment.CENTER);
            document.Add(title);
            ImageData imageData = ImageDataFactory.Create(tour.StaticMap);
            document.Add(new Image(imageData));
            Paragraph description = new Paragraph("Description: " + tour.Description).SetFontSize(16);
            document.Add(description);
            Paragraph fromto = new Paragraph("Route: From " + tour.From + " to " + tour.To).SetFontSize(16);
            Paragraph distance = new Paragraph("Distance: " + tour.Distance).SetFontSize(16);
            Paragraph estTime = new Paragraph("Estimated Time: " + tour.EstimatedTime + " min").SetFontSize(16);
            Paragraph transportType = new Paragraph("Transport type: " + tour.TransportType).SetFontSize(16);
            document.Add(fromto);
            document.Add(distance);
            document.Add(estTime);
            document.Add(transportType);
            document.Add(new Paragraph("\n"));

            AddLogsToDocument(document, tour.Logs);
            
            document.Close();
        }

        public void GenerateSummaryReport(ObservableCollection<Tour> tours, string path)
        {
            var writer = new PdfWriter(path);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument);

            Paragraph title = new Paragraph("Summary report").SetFontSize(28).SetBold().SetTextAlignment(TextAlignment.CENTER);
            document.Add(title);
            
            foreach(Tour tour in tours)
            {
                document.Add(new Paragraph("\n"));
                Paragraph tourName = new Paragraph(tour.Name).SetFontSize(20).SetBold();
                document.Add(tourName);

                if(tour.Logs != null)
                {
                    if(tour.Logs.Count > 0)
                    {
                        Paragraph logCount = new Paragraph("Amount of logs: " + tour.Logs.Count).SetFontSize(16);
                        document.Add(logCount);

                        Table table = new Table(UnitValue.CreatePercentArray(6)).UseAllAvailableWidth();
                        table.AddHeaderCell("Average time");
                        table.AddHeaderCell("Average difficulty");
                        table.AddHeaderCell("Average rating");
                        double sumTime = 0;
                        double sumDifficulty = 0;
                        double sumRating = 0;
                        foreach (TourLog log in tour.Logs)
                        {
                            sumTime += log.TotalTime;
                            sumDifficulty += log.Difficulty;
                            sumRating += log.Rating;
                        }
                        double avgTime = Math.Round(sumTime / tour.Logs.Count, 2);
                        double avgDifficulty = Math.Round(sumDifficulty / tour.Logs.Count, 2);
                        double avgRating = Math.Round(sumRating / tour.Logs.Count, 2);

                        table.AddCell(avgTime.ToString() + " min");
                        table.AddCell(avgDifficulty.ToString());
                        table.AddCell(avgRating.ToString());

                        document.Add(table);
                    }
                    else
                    {
                        document.Add(new Paragraph("No logs available for this tour!").SetFontSize(16));
                    }
                }
                else
                {
                    document.Add(new Paragraph("No logs available for this tour!").SetFontSize(16));
                }
            }
            document.Close();
        }

        private void AddLogsToDocument(Document document, ObservableCollection<TourLog> logs)
        {
            document.Add(new Paragraph("Logs").SetFontSize(18).SetBold());
            if (logs != null)
            {
                if(logs.Count > 0)
                {
                    Table table = new Table(UnitValue.CreatePercentArray(6)).UseAllAvailableWidth();
                    table.AddHeaderCell("Name");
                    table.AddHeaderCell("Comment");
                    table.AddHeaderCell("Date");
                    table.AddHeaderCell("Difficulty");
                    table.AddHeaderCell("Time");
                    table.AddHeaderCell("Rating");

                    foreach (TourLog log in logs)
                    {
                        table.AddCell(log.Name);
                        table.AddCell(log.Comment);
                        table.AddCell(log.DateTime.ToString());
                        table.AddCell(log.Difficulty.ToString());
                        table.AddCell(log.TotalTime.ToString());
                        table.AddCell(log.Rating.ToString());
                    }

                    document.Add(table);
                }
                else
                {
                    document.Add(new Paragraph("No logs available!").SetFontSize(16));
                }
            }
            else
            {
                document.Add(new Paragraph("No logs available!").SetFontSize(16));
            }
        }
    }
}
