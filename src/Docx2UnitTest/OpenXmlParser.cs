using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Text.RegularExpressions;
using System.Xml;
using Docx2UnitTest.CodeGeneration;
using Docx2UnitTest.FrameworkExtensions;
using Docx2UnitTest.Model;

namespace Docx2UnitTest
{
    internal class OpenXmlParser
    {
        internal static ITestFramework GetTestFramework(string filePath)
        {
            var wordXml = ExtractXmlFromWordDocument(filePath);

            // Select all paragraphs.
            var paragraphs = wordXml.GetElementsByTagName("w:p");

            ITestFramework testFramework = null;

            TestClassModel testClass = null;

            for (var i = 0; i < paragraphs.Count; i++)
            {
                var paragraph = paragraphs[i];

                if (IsDocumentHeadline(paragraph)) continue;
                if (string.IsNullOrWhiteSpace(paragraph.InnerText)) continue;

                ITestFramework tf;
                if ((tf = GetTestFremework(paragraph.InnerText)) != null)
                {
                    testFramework = tf;
                }
                
                if (!paragraph.InnerXml.Contains("ListParagraph") &&
                    testFramework != null)
                {
                    testClass =
                        testFramework.CreateTestClassModel(
                            paragraph.InnerText.GetClearName());
                    continue;                       
                }
                
                if (paragraph.InnerXml.Contains("ListParagraph") &&  
                    testClass != null)
                {
                    testClass.Tests.Add(
                        testFramework.CreateTestModel(
                            paragraph.InnerText.GetClearName()));
                }
            }

            return testFramework;
        }

        private static XmlDocument ExtractXmlFromWordDocument(string filePath)
        {
            using (var package = Package.Open(filePath, FileMode.Open))
            {
                var packagePart = 
                    package.GetPart(
                        new Uri("/word/document.xml", 
                            UriKind.Relative));

                using (var stream = packagePart.GetStream())
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(stream);

                    return xmlDoc.DocumentElement != null ? xmlDoc : null;
                }
            }
        }

        private static bool IsDocumentHeadline(XmlNode node)
        {
            return Regex.IsMatch(node.InnerXml, "w:val=\"Heading[0-9]{1,}\"", RegexOptions.Compiled);
        }

        private static readonly Regex s_testFrameworkRegex =
           new Regex("Testing framework: (?<TestFramework>.{1,})",
             RegexOptions.Compiled);


        private static ITestFramework GetTestFremework(string input)
        {
            var match = s_testFrameworkRegex.Match(input);
            if (match.Success) return null;

            var testFramework = match.Groups["TestFramework"].Value;
            switch (testFramework.ToLowerInvariant())
            {
                case "nunit":
                    return new NUnitFramework();

                case "mstest":
                    return new MsTestFramework();

                case "xunit":
                    return new XUnitFramework();

                default:
                    return null;
            }

        }

        private static bool IsTargetProjectName(string input)
        {
            Match match = Regex.Match(input, "Target project: (?<TargetProject>.{1,})", RegexOptions.Compiled);

            if (match.Success)
            {
                // TODO
                // string projectName = match.Groups["TargetProject"].Value;
            }

            return match.Success;
        }
    }
}