namespace InventoryManagement.Commons.Extensions
{
    public static class VietnamCurrencyExtension
    {
        public static string FormatVietnameseCurrency(float amount)
        {
            return amount.ToString("#,##0.00").Replace(",", "!").Replace(".", ",").Replace("!", ".") + " ₫";
        }
    }
}
