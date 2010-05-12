using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using devplex.Tools.Model;
using EnvDTE;

namespace devplex.Tools
{
    /// <summary>
    /// Parser for existing project items.
    /// </summary>
    internal static class TestFileModelParser
    {
        private static readonly Regex s_codeRegex = 
            new Regex(
                @"\r\n\t\t(?<attributes>[\[|\]|a-z|A-Z|0-9|\(|\)|\&|\<|\>|\/|\s|\r\n\t]*)\r\n\t\tpublic\svoid\s(?<testname>[A-Z|a-z|0-9|_]*)\(\)\r\n\t\t\{(?<code>[a-z|A-Z|\s|\n|\r|\||\!|\&|\+|\~|\{|\}|\=|\""|\'|;|\<|\>|\*|\.|\,|\(|\)|\@|\[|\]|\\|\/]*)\r\n\t\t\}", 
                RegexOptions.Compiled);

        private static readonly Regex s_usingRegex = 
            new Regex(
                @"using\s(?<namespace>[a-z|A-Z|0-9|_|\-|\.]{3,200})\;", 
                RegexOptions.Compiled);

        #region UpdateModel(ProjectItems projectItems, ITestFramework fw)
         /// <summary>
        /// Updates the model.
        /// </summary>
        /// <param name="projectItems">The project items.</param>
        /// <param name="fw">The fw.</param>
        public static void UpdateModel(
            ProjectItems projectItems,
            ITestFramework fw)
        {
            for (var i = 1; i < projectItems.Count - 1; i++)
            {
                var projectItem = projectItems.Item(i);

                if (projectItem.Name.EndsWith(
                    ".log",
                    StringComparison.OrdinalIgnoreCase)) continue;

                var testClass =
                    (from tc in fw.Classes
                     where tc.Name == projectItem.Name.Replace(".cs", "")
                     select tc).FirstOrDefault();

                if (testClass == null)
                {
                    //todo: dump code to log....
                    continue;
                }

                string fileContent;
                using (var sr = new StreamReader(projectItem.FileNames[0]))
                {
                    fileContent = sr.ReadToEnd();
                }

                var usingMatches = s_usingRegex.Matches(fileContent);
                for (var j = 0; j < usingMatches.Count; j++)
                {
                    var match = usingMatches[j];
                    if (!testClass.UsingStatements.Contains(match.Value))
                    {
                        testClass.UsingStatements.Add(match.Value);
                    }
                }

                var matches = s_codeRegex.Matches(fileContent);
                for (var j = 0; j < matches.Count; j++)
                {
                    var match = matches[j];

                    if (!match.Success) continue;

                    var testName = match.Groups["testname"].Value;
                    var testAttributes = match.Groups["attributes"].Value;
                    var testImplementation = match.Groups["code"].Value;

                    var test =
                        (from t in testClass.Tests
                         where t.Name == testName
                         select t).FirstOrDefault();

                    if (test == null)
                    {
                        //todo: dump code to log....
                        continue;
                    }

                    test.Attributes = testAttributes;
                    test.Implementation = testImplementation;
                }
            }
        } 
        #endregion
    }
}
