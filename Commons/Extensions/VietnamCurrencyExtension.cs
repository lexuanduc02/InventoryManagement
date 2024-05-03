namespace InventoryManagement.Commons.Extensions
{
    public static class VietnamCurrencyExtension
    {
        public static string FormatVietnameseCurrency(this float amount)
        {
            return amount.ToString("#,##0.00").Replace(",", "!").Replace(".", ",").Replace("!", ".") + " VND";
        }
    }
}
