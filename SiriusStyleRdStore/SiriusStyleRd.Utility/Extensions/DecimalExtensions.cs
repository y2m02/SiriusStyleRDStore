namespace SiriusStyleRd.Utility.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToNumericFormat(this decimal number, int decimals = 2)
        {
            return number.ToString($"N{decimals}");
        }
    }
}