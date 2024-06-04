using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using PdfSharp.Drawing.Layout;

namespace BookingApp.Application.UseCases
{
    class PDFTourReservationGenerator
    {
        public void GenerateAndSavePdf(List<TourGuest> guests, int voucherId, int numberOfPeople, string touristUsername, TourDTO selectedTour, KeyValuePair<int, string>? selectedStartTime)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Tour Reservation Report";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Draw the background image
            string backgroundPath = @"C:\Users\Mateja\Documents\aFakultet\sesti_semestar\sims\projekat\Resources\Images\Tourist\background.png";
            XImage background = XImage.FromFile(backgroundPath);
            gfx.DrawImage(background, 0, 0, page.Width, page.Height);

            // Create fonts
            XFont titleFont = new XFont("Times New Roman", 20, XFontStyleEx.Regular);
            XFont regularFont = new XFont("Times New Roman", 12, XFontStyleEx.Regular);
            XFont boldFont = new XFont("Times New Roman", 12, XFontStyleEx.Bold);

            // Draw the logo image in the upper left corner
            string logoPath = @"C:\Users\Mateja\Documents\aFakultet\sesti_semestar\sims\projekat\Resources\Images\Tourist\logo.png";
            XImage logo = XImage.FromFile(logoPath);
            gfx.DrawImage(logo, 10, 40, 70, 70);  // Moved down

            // Draw the title text centered
            gfx.DrawString("Izveštaj o rezervaciji ture", titleFont, XBrushes.Black,
                new XRect(0, 60, page.Width, 40),
                XStringFormats.Center);

            // Draw the issuer and date information
            string dateIssued = $"{DateTime.Now:dd.MM.yyyy}";
            string issuer = "Mateja Booking Agencija";
            string recipient = $"{touristUsername}";

            gfx.DrawString("Datum izdavanja izveštaja:", boldFont, XBrushes.Black,
                new XPoint(10, 120), XStringFormats.TopLeft);
            gfx.DrawString(dateIssued, regularFont, XBrushes.Black,
                new XPoint(150, 120), XStringFormats.TopLeft);

            gfx.DrawString("Izdavač:", boldFont, XBrushes.Black,
                new XPoint(10, 140), XStringFormats.TopLeft);
            gfx.DrawString(issuer, regularFont, XBrushes.Black,
                new XPoint(150, 140), XStringFormats.TopLeft);

            gfx.DrawString("Primalac:", boldFont, XBrushes.Black,
                new XPoint(10, 160), XStringFormats.TopLeft);
            gfx.DrawString(recipient, regularFont, XBrushes.Black,
                new XPoint(150, 160), XStringFormats.TopLeft);

            // Draw a horizontal line
            gfx.DrawLine(new XPen(XColors.Black, 4), new XPoint(10, 180), new XPoint(page.Width - 10, 180));

            // Draw the reservation information
            gfx.DrawString($"{touristUsername} je rezervisao turu za {numberOfPeople} osoba koja će se održati {selectedStartTime?.Value}.", regularFont, XBrushes.Black,
                new XRect(10, 190, page.Width - 20, 20),
                XStringFormats.TopLeft);

            gfx.DrawString("Naziv ture:", boldFont, XBrushes.Black,
                new XPoint(10, 220), XStringFormats.TopLeft);
            gfx.DrawString(selectedTour.Name, regularFont, XBrushes.Black,
                new XPoint(150, 220), XStringFormats.TopLeft);

            gfx.DrawString("Datum održavanja ture:", boldFont, XBrushes.Black,
                new XPoint(10, 240), XStringFormats.TopLeft);
            gfx.DrawString(selectedStartTime?.Value, regularFont, XBrushes.Black,
                new XPoint(150, 240), XStringFormats.TopLeft);

            gfx.DrawString("Trajanje ture u satima:", boldFont, XBrushes.Black,
                new XPoint(10, 260), XStringFormats.TopLeft);
            gfx.DrawString(selectedTour.DurationHours.ToString(), regularFont, XBrushes.Black,
                new XPoint(150, 260), XStringFormats.TopLeft);

            gfx.DrawString("Opis ture:", boldFont, XBrushes.Black,
                new XPoint(10, 280), XStringFormats.TopLeft);

            // Define a rectangle for the tour description
            XRect descriptionRect = new XRect(10, 300, page.Width - 20, page.Height - 300 - 60);

            // Use XTextFormatter to draw the tour description within the rectangle
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(selectedTour.Description, regularFont, XBrushes.Black, descriptionRect);

            // Measure the height of the description text block
            double descriptionHeight = MeasureDescriptionHeight(gfx, selectedTour.Description, regularFont, descriptionRect.Width);
            int yOffset = (int)descriptionRect.Y + (int)descriptionHeight + 10; // Add a small margin

            gfx.DrawString("Turisti:", boldFont, XBrushes.Black,
                new XPoint(10, yOffset), XStringFormats.TopLeft);

            // Draw the list of guests
            yOffset += 20; // Remove this line to fix the double margin issue
            for (int i = 0; i < guests.Count; i++)
            {
                var guest = guests[i];
                gfx.DrawString($"{i + 1}.", boldFont, XBrushes.Black,
                    new XPoint(20, yOffset), XStringFormats.TopLeft);
                gfx.DrawString($"{guest.Name}, {guest.Age} godina", regularFont, XBrushes.Black,
                    new XPoint(50, yOffset), XStringFormats.TopLeft);
                yOffset += 20;
            }

            if (voucherId != -1)
            {
                gfx.DrawString("Vaučer iskorišćen", boldFont, XBrushes.Black,
                    new XPoint(10, yOffset + 20), XStringFormats.TopLeft);
            }

            // Save the document
            SavePdfDocument(document);
        }

        private double MeasureDescriptionHeight(XGraphics gfx, string text, XFont font, double maxWidth)
        {
            var size = gfx.MeasureString(text, font);
            int lines = (int)Math.Ceiling(size.Width / maxWidth);
            return lines * size.Height;
        }

        private double MeasureGuestsHeight(XGraphics gfx, List<TourGuest> guests, XFont font, double maxWidth)
        {
            double totalHeight = 0;
            foreach (var guest in guests)
            {
                var text = $"{guest.Name}, {guest.Age} godina";
                var size = gfx.MeasureString(text, font);
                totalHeight += size.Height;
            }
            return totalHeight;
        }

        private void SavePdfDocument(PdfDocument document)
        {
            // Open a file dialog to specify where to save the PDF file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Save PDF Report",
                FileName = "izvestajRezervacija.pdf"
            };

            // Show the dialog and check if the user selected a file
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;

                // Save the PDF document to the specified file
                document.Save(filename);

                // Open the PDF document in the default viewer
                Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });
            }
        }
    }
}