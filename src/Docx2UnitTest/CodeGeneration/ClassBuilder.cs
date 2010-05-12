using System;
using System.Text;
using devplex.Tools.Model;

namespace devplex.Tools.CodeGeneration
{
    /// <summary>
    /// Class builder.
    /// </summary>
    internal class ClassBuilder
    {
        #region  CreateClass(TestClassModel testClass)
        /// <summary>
        /// Creates the class.
        /// </summary>
        /// <param name="testClass">The test class.</param>
        /// <returns></returns>
        internal static byte[] CreateClass(TestClassModel testClass)
        {
            var stringBuilder = new StringBuilder();

            foreach (var requiredUsing in testClass.UsingStatements)
            {
                stringBuilder.AppendLine(requiredUsing);
            }
            stringBuilder.AppendLine("");

            stringBuilder.Append("namespace ");
            stringBuilder.AppendLine(testClass.Namespace);
            stringBuilder.AppendLine("{");

            stringBuilder.Append("\t");

            stringBuilder.AppendLine(
                testClass.Attributes.TrimEnd('\r').TrimEnd('\n'));

            stringBuilder.AppendLine(String.Concat("\tpublic class ", testClass.Name));
            stringBuilder.AppendLine("\t{");

            foreach (var test in testClass.Tests)
            {
                stringBuilder.Append("\t\t");
                stringBuilder.AppendLine(test.Attributes.TrimEnd('\n').TrimEnd('\r').TrimEnd('\n'));

                stringBuilder.Append("\t\tpublic void ");
                stringBuilder.Append(test.Name);
                stringBuilder.AppendLine("()");
                stringBuilder.Append("\t\t{");
                stringBuilder.AppendLine(test.Implementation);
                stringBuilder.AppendLine("\t\t}");

                stringBuilder.AppendLine("");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.AppendLine("\t}"); // class

            stringBuilder.AppendLine("}"); // namespace
            return Encoding.UTF8.GetBytes(stringBuilder.ToString());
        } 
        #endregion
    }
}