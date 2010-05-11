using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Xml;
using BetterCode.Tools.Common;

namespace BetterCode.Tools
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
    }
}