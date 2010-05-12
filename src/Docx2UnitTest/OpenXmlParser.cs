using System;
using System.IO;
using System.IO.Packaging;
using System.Text.RegularExpressions;
using System.Xml;
using devplex.Tools.FrameworkExtensions;
using devplex.Tools.Model;

namespace devplex.Tools
{
    /// <summary>
    /// Parser for Open XML packages.
    /// </summary>
    internal class OpenXmlParser
    {
        private static readonly Regex s_testFrameworkRegex =
           new Regex("Testing framework: (?<TestFramework>.{1,})",
             RegexOptions.Compiled);

        private static readonly Regex s_targetProjectRegex =
            new Regex(
                "Target project: (?<TargetProject>.{1,})",
                RegexOptions.Compiled);

        private static readonly Regex s_headlineRegex =
            new Regex(
                "w:val=\"Heading[0-9]{1,}\"", 
                RegexOptions.Compiled);

        #region GetTestFramework(string filePath, string clrNamespace)
        /// <summary>
        /// Gets the test framework.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="clrNamespace">The CLR namespace.</param>
        /// <returns></returns>
        internal static ITestFramework GetTestFramework(
            string filePath,
            string clrNamespace)
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
                    testFramework.Namespace = clrNamespace;
                    continue;
                }

                string targetProject;
                if ((targetProject =
                    TargetProjectName(paragraph.InnerText)) != null &&
                    testFramework != null)
                {
                    testFramework.TargetProject = targetProject;
                }

                if (!paragraph.InnerXml.Contains("ListParagraph") &&
                    testFramework != null)
                {
                    testClass =
                        testFramework.CreateTestClassModel(
                            paragraph.InnerText.GetClearName());
                    testFramework.Classes.Add(testClass);
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
        #endregion

        #region ExtractXmlFromWordDocument(string filePath)
        /// <summary>
        /// Extracts the XML from word document.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
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
        #endregion

        #region IsDocumentHeadline(XmlNode node)
        /// <summary>
        /// Determines whether [is document headline] [the specified node].
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        /// 	<c>true</c> if [is document headline] [the specified node]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsDocumentHeadline(XmlNode node)
        {
            return s_headlineRegex.IsMatch(node.InnerXml);
        } 
        #endregion

        #region GetTestFremework(string input)
        /// <summary>
        /// Gets the test fremework.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static ITestFramework GetTestFremework(string input)
        {
            var match = s_testFrameworkRegex.Match(input);
            if (!match.Success) return null;

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
        #endregion

        #region TargetProjectName(string input)
        /// <summary>
        /// Targets the name of the project.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static string TargetProjectName(string input)
        {
            var match = s_targetProjectRegex.Match(input);
            return match.Success ? match.Groups["TargetProject"].Value : null;
        } 
        #endregion
    }
}