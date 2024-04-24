using InventoryManagement.Services.Contractors;
using SelectPdf;

namespace InventoryManagement.Services
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> HtmlToPdf(string html)
        {
            HtmlToPdf converter = new HtmlToPdf();

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;

            PdfDocument doc = converter.ConvertHtmlString(html);

            // save pdf document
            byte[] bytes = doc.Save();

            // close pdf document
            doc.Close();

            return bytes;
        }
    }
}
