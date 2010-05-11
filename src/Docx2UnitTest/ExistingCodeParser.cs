using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using EnvDTE;

namespace Docx2UnitTest
{
    public static class ExistingCodeParser
    {
        private static readonly Regex _codeRegex = 
            new Regex(
                @"public\svoid\s(?<testname>[A-Z|a-z|0-9|_]*)\(\)\r\n\s(?<tab>[\s|\t]{2,9})\{(?<code>[a-z|A-Z|\s|\n|\r|\{|\}|\=|\""|\'|;|\<|\>|\.|\,|\(|\)|\@|\[|\]|\\|\/]*)\r\n(?<end>[\s|\t]{2,9}\})", 
                RegexOptions.Compiled);

        public static Dictionary<string, Dictionary<string, string>> GetExistingCode(
            ProjectItems projectItems)
        {
            var dictionary = 
                new Dictionary<string, Dictionary<string, string>>();


            for (var i = 0; i < projectItems.Count; i++)
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

                dictionary.Add(
                    projectItem.Name, 
                    new Dictionary<string, string>()); 

                for (var j = 0; j < matches.Count; j++)
                {
                    var match = matches[j];

                    if (!match.Success) continue;

                    var testName = match.Groups["testname"].Value;
                    var testImplementation = match.Groups["code"].Value;

                    dictionary[projectItem.Name].Add(testName, testImplementation);
                }
            }

            return dictionary;
        }
    }
}
