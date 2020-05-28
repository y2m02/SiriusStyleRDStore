namespace SiriusStyleRdStore.Utility.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsEmpty<T>(this T obj)
        {
            return obj == null;
        }
        
        public static bool HasValue<T>(this T obj)
        {
            return !IsEmpty(obj);
        }
    }
}