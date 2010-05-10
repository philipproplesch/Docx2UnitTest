using System;

namespace BetterCode.Tools
{
    internal class Logger
    {
        internal static string Message { get; private set; }

        internal static void Write(string message)
        {
            Message += string.Concat(message, Environment.NewLine);
        }
    }
}