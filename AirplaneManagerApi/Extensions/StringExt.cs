using System;
using System.Text.RegularExpressions;
namespace projekt.Extensions
{
    public static class StringExt
    {
        public static bool IsSpecialCharFree(this string str)
            => Regex.IsMatch(str, @"^[a-zA-Z0-9 ]*$");

        public static bool IsNullOrEmpty(this string str)
            => string.IsNullOrEmpty(str);

        public static bool IsNumeric(this string str)
            => int.TryParse(str, out int num);
    }
}

