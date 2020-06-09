using System;
using System.Collections.Generic;

namespace SiriusStyleRdStore.Utility.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToFormattedString(this DateTime date, DateFormat format = DateFormat.MMddyyyy)
        {
            return date.ToString(GetFormat(format));
        }

        public static string ToFormattedString(this DateTime? date, DateFormat format = DateFormat.MMddyyyy)
        {
            return date.IsEmpty()
                ? ""
                : ((DateTime) date).ToFormattedString(format);
        }

        private static string GetFormat(DateFormat type)
        {
            var map = new Dictionary<DateFormat, string>
            {
                [DateFormat.ddMMyyyy] = "dd/MM/yyyy",
                [DateFormat.MMddyyyy] = "mm/dd/yyyy",
            };

            return map[type];
        }
    }

    public enum DateFormat
    {
        ddMMyyyy,
        MMddyyyy
    }
}
