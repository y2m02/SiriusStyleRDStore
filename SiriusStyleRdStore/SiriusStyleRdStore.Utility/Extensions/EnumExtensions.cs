using System;
using System.ComponentModel;
using System.Linq;

namespace BillingSystem.Utility.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum @enum)
        {
            if (@enum.IsEmpty()) return "";

            var fieldInfo = @enum.GetType().GetField(@enum.ToString());

            var attribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>().Single();

            return attribute?.Description;
        }

        public static string EnumToString(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }
    }
}