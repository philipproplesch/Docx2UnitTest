using System;
using System.Text;
using Docx2UnitTest.Model;

namespace Docx2UnitTest.CodeGeneration
{
    internal class ClassBuilder
    {
        internal static byte[] CreateClass(TestClassModel testClass)
        {
            var stringBuilder = new StringBuilder();

            foreach (var requiredUsing in testClass.UsingStatements)
            {
                stringBuilder.AppendLine(requiredUsing);
            }
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine(
                testClass.Attributes.TrimEnd('\r').TrimEnd('\n'));
            
            stringBuilder.AppendLine(String.Concat("public class ", testClass.Name));
            stringBuilder.AppendLine("{");

            foreach (var test in testClass.Tests)
            {
                //todo: add tab for namespace indent
                stringBuilder.Append("\t");
                stringBuilder.AppendLine(test.Attributes.TrimEnd('\n').TrimEnd('\r').TrimEnd('\n'));

                stringBuilder.Append("\tpublic void ");
                stringBuilder.Append(test.Name);
                stringBuilder.AppendLine("()");
                stringBuilder.Append("\t{");
                stringBuilder.AppendLine(test.Implementation);
                stringBuilder.AppendLine("\t}");
                
                stringBuilder.AppendLine("");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.AppendLine("}");

            return Encoding.UTF8.GetBytes(stringBuilder.ToString());
        }
    }
}