using System;
using System.Text.RegularExpressions;

namespace TelerikBlazorDemos.Extensions
{
    public static class StringExtensions
    {
        public static string ToPascalCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
        }

        public static DateTime ParseEpochSecondsToDateTime(this string value)
        {
            var epochSeconds = Regex.Match(value, @"[0-9]+").Value;
            DateTime date = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(epochSeconds)).DateTime;
            return date;
        }
    }
}
