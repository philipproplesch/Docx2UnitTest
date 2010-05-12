using System.Text.RegularExpressions;

namespace devplex.Tools.FrameworkExtensions
{
    /// <summary>
    /// Extensions for the string class.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Gets the name of the clear.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        internal static string GetClearName(this string instance)
        {
            return Regex.Replace(instance, @"[\W]{1,}", "_");
        }
    }
}