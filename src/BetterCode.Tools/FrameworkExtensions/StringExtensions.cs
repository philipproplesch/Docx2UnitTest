using System.Text.RegularExpressions;

namespace BetterCode.Tools.FrameworkExtensions
{
    internal static class StringExtensions
    {
        internal static string GetClearName(this string instance)
        {
            return Regex.Replace(instance, @"[\W]{1,}", "_");
        }
    }
}