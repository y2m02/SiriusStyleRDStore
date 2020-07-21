namespace SiriusStyleRd.Utility.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToNumericFormat(this decimal number, int decimals = 2)
        {
            return number.ToString($"N{decimals}");
        }

        public static string ToNumericFormat(this decimal? number, int decimals = 2)
        {
            return number.IsEmpty()
                ? "0.00"
                : ((decimal)number).ToNumericFormat(decimals);
        }
    }
}