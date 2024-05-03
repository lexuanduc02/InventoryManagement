namespace InventoryManagement.Services.Contractors
{
    public interface IPdfService
    {
        public Task<byte[]> HtmlToPdf(string html);
    }
}
