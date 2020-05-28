namespace SiriusStyleRdStore.Utility.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsEmpty(this string str)
        {
            return str.IsNullOrEmpty() || str.IsNullOrWhiteSpace();
        }

        public static int ToInt(this string str)
        {
            return int.TryParse(str, out var n)
                ? n
                : 0;
        }
    }
}