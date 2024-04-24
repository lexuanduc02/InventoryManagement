namespace InventoryManagement.Commons.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToDateOnly(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
