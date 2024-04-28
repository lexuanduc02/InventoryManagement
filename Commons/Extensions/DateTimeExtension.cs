namespace InventoryManagement.Commons.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToDateOnly(this DateTime dateTime)
        {
            if(dateTime == DateTime.MinValue)
            {
                return "";
            }

            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
