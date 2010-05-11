using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Docx2UnitTest.Model;
using EnvDTE;

namespace BetterCode.Tools
{
    internal static class TestFileModelParser
    {
        private static readonly Regex _codeRegex = 
            new Regex(
                @"\r\n\t(?<attributes>[\[|\]|a-z|A-Z|0-9|\(|\)|\&|\<|\>|\/|\s|\r\n\t]*)\r\n\tpublic\svoid\s(?<testname>[A-Z|a-z|0-9|_]*)\(\)\r\n\t\{(?<code>[a-z|A-Z|\s|\n|\r|\||\!|\&|\+|\~|\{|\}|\=|\""|\'|;|\<|\>|\.|\,|\(|\)|\@|\[|\]|\\|\/]*)\r\n\t\}", 
                RegexOptions.Compiled);

        private static readonly Regex _usingRegex = 
            new Regex(
                @"using\s(?<namespace>[a-z|A-Z|0-9|_|\-|\.]{3,200})\;", 
                RegexOptions.Compiled);

        public static void UpdateModel(
            ProjectItems projectItems,
            ITestFramework fw)
        {
            for (var i = 1; i < projectItems.Count-1; i++)
            {
                var projectItem = projectItems.Item(i);

                if (projectItem.Name.EndsWith(
                    ".log",
                    StringComparison.OrdinalIgnoreCase)) continue;

                var fileContent = string.Empty;
                using(var sr = new StreamReader(projectItem.FileNames[0]))
                {
                    fileContent = sr.ReadToEnd();
                }

                var matches = _codeRegex.Matches(fileContent);

                var testClass =
                    (from tc in fw.Classes
                     where tc.Name == projectItem.Name.Replace(".cs", "")
                     select tc).FirstOrDefault();

                if(testClass == null)
                {
                    //todo: dump code to log....
                    continue;
                }

                for (var j = 0; j < matches.Count; j++)
                {
                    var match = matches[j];

                    if (!match.Success) continue;

                    var testName = match.Groups["testname"].Value;
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

                    test.Implementation = testImplementation;
                }
            }
        }
    }
}
