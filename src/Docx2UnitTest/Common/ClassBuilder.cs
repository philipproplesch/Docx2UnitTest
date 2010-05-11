using System;
using System.Text;
using Docx2UnitTest.FrameworkExtensions;
using Docx2UnitTest.TestFrameworks;

namespace Docx2UnitTest.Common
{
    internal class ClassBuilder
    {
        public static ITestFramework TestFramework;

        internal static byte[] CreateClass(TestClass testClass)
        {
            var stringBuilder = new StringBuilder();

            // Add "usings"
            foreach (string requiredUsing in TestFramework.RequiredUsings)
            {
                stringBuilder.AppendLine(string.Format("using {0};", requiredUsing));
            }
            stringBuilder.AppendLine("");

            // Add class attributes.
            foreach (string classAttribute in TestFramework.ClassAttributes)
            {
                stringBuilder.AppendLine(classAttribute);
            }
            stringBuilder.AppendLine(String.Concat("public class ", testClass.ClassName.GetClearName()));
            stringBuilder.AppendLine("{");

            for (int i = 0; i < testClass.MethodNames.Count; i++)
            {
                string methodName = testClass.MethodNames[i];

                // Add method attributes.
                foreach (string methodAttribute in TestFramework.MethodAttributes)
                {
                    stringBuilder.AppendLine(string.Concat("\t", methodAttribute));
                }
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