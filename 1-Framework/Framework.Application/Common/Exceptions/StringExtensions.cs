using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Application.Common.Exceptions
{
    public static class StringExtensions
    {
        internal static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }
    }
}
