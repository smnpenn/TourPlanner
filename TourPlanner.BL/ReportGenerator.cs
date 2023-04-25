using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
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
            document.Add(new Paragraph(tour.Name));
            ImageData imageData = ImageDataFactory.Create(tour.StaticMap);
            document.Add(new Image(imageData));
            document.Close();
        }
    }
}
