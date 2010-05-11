using System.Text.RegularExpressions;

namespace Docx2UnitTest.FrameworkExtensions
{
    internal static class StringExtensions
    {
        internal static string GetClearName(this string instance)
        {
            return Regex.Replace(instance, @"[\W]{1,}", "_");
        }
    }
}