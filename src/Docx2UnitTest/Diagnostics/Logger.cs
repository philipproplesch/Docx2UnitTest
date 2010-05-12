using System.Text;

namespace devplex.Tools.Diagnostics
{
    /// <summary>
    /// A logger.
    /// </summary>
    internal class Logger
    {
        private static readonly StringBuilder s_messageBuilder = 
            new StringBuilder();

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        internal static void Write(string message)
        {
            s_messageBuilder.AppendLine(message);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        internal static string Message
        {
            get { return s_messageBuilder.ToString(); }
        }
    }
}