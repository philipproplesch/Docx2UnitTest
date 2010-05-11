using System;

namespace Docx2UnitTest
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