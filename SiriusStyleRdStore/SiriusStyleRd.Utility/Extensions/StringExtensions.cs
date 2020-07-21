using System;
using System.ComponentModel;

namespace SiriusStyleRd.Utility.Extensions
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

        public static T ToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }

        public static T GetEnumValueFromDescription<T>(this string str)
        {
            if (str.IsEmpty()) return default;

            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == str)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == str)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(str));
            // or return default(T);
        }
    }
}