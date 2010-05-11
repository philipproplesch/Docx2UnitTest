using System;
using System.Text;
using Docx2UnitTest.FrameworkExtensions;

namespace Docx2UnitTest.Common
{
    internal class ClassBuilder
    {
        internal static byte[] CreateClass(TestClass testClass)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("using NUnit.Framework;");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("[TestFixture]");
            stringBuilder.AppendLine("[Category(\"\")]");
            stringBuilder.AppendLine(String.Concat("public class ", testClass.ClassName.GetClearName()));
            stringBuilder.AppendLine("{");

            for (int i = 0; i < testClass.MethodNames.Count; i++)
            {
                string methodName = testClass.MethodNames[i];
                stringBuilder.AppendLine("\t[Test]");
                stringBuilder.AppendLine(String.Format("\tpublic void {0}()", methodName.GetClearName()));
                stringBuilder.AppendLine("\t{");
                stringBuilder.AppendLine("\t\tthrow new NotImplementedException();");
                stringBuilder.AppendLine("\t}");
                if (i < testClass.MethodNames.Count - 1)
                    stringBuilder.AppendLine("");
            }

            stringBuilder.AppendLine("}");

            return Encoding.UTF8.GetBytes(stringBuilder.ToString());
        }
    }
}