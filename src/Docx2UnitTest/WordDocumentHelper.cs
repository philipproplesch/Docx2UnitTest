using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Text.RegularExpressions;
using System.Xml;
using Docx2UnitTest.Common;
using Docx2UnitTest.TestFrameworks;

namespace Docx2UnitTest
{
    internal class WordDocumentHelper
    {
        internal static List<TestClass> GetTestSections(string filePath)
        {
            XmlDocument wordXml = ExtractXmlFromWordDocument(filePath);

            // Select all paragraphs.
            XmlNodeList paragraphs = wordXml.GetElementsByTagName("w:p");

            var testSections = new List<TestClass>();

            TestClass testSection = null;
            for (int i = 0; i < paragraphs.Count; i++)
            {
                XmlNode paragraph = paragraphs[i];

                if (IsDocumentHeadline(paragraph) || IsTestFrameworkDefinition(paragraph.InnerText) ||
                    /*IsTargetProjectName(paragraph.InnerText) ||*/ string.IsNullOrWhiteSpace(paragraph.InnerText))
                    continue;
                
                if (!paragraph.InnerXml.Contains("ListParagraph"))
                {
                    if (testSection != null)
                        testSections.Add(testSection);

                    testSection = new TestClass {MethodNames = new List<string>(), ClassName = paragraph.InnerText};
                }
                else
                {
                    if (testSection != null)
                        testSection.MethodNames.Add(paragraph.InnerText);
                }

                if (i == paragraphs.Count - 1)
                    testSections.Add(testSection);
            }

            return testSections;
        }

        private static XmlDocument ExtractXmlFromWordDocument(string filePath)
        {
            using (Package package = Package.Open(filePath, FileMode.Open))
            {
                PackagePart packagePart = package.GetPart(new Uri("/word/document.xml", UriKind.Relative));

                Stream stream = packagePart.GetStream();
                stream.Seek(0, SeekOrigin.Begin);

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);

                return xmlDoc.DocumentElement != null ? xmlDoc : null;
            }
        }

        private static bool IsDocumentHeadline(XmlNode node)
        {
            return Regex.IsMatch(node.InnerXml, "w:val=\"Heading[0-9]{1,}\"", RegexOptions.Compiled);
        }

        private static bool IsTestFrameworkDefinition(string input)
        {
            Match match = Regex.Match(input, "Testing framework: (?<TestFramework>.{1,})", RegexOptions.Compiled);

            if (match.Success)
            {
                string testFramework = match.Groups["TestFramework"].Value;
                switch (testFramework.ToLowerInvariant())
                {
                    case "nunit":
                        ClassBuilder.TestFramework = new NUnitFramework();
                        break;
                    case "mstest":
                        ClassBuilder.TestFramework = new MSTestFramework();
                        break;
                    case "xunit":
                        ClassBuilder.TestFramework = new XUnitFramework();
                        break;
                }
            }

            return match.Success;
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