using System.Text;

namespace Docx2UnitTest.Common
{
    internal class Logger
    {
        private static readonly StringBuilder s_messageBuilder = 
            new StringBuilder();

        internal static void Write(string message)
        {
            s_messageBuilder.AppendLine(message);
        }

        public static string Message
        {
            get { return s_messageBuilder.ToString(); }
        }
    }
}