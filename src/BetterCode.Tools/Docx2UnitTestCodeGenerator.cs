using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using BetterCode.Tools.Common;
using BetterCode.Tools.FrameworkExtensions;
using EnvDTE;
using Microsoft.CustomTool;

namespace BetterCode.Tools
{
    [ComVisible(true)]
    [Guid("3b75d10d-2892-49d9-9939-7d13b56f55ca")]
    public class Docx2UnitTestCodeGenerator : BaseCodeGeneratorWithSite
    {
        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            var projectItem = GetService(typeof (ProjectItem)) as ProjectItem;
            if (projectItem != null)
            {
                string projectItemPath = projectItem.get_FileNames(0);
                string fileDestination = Path.GetDirectoryName(inputFileName);

                List<TestClass> testSections = WordDocumentHelper.GetTestSections(projectItemPath);

                foreach (TestClass testSection in testSections)
                {
                    string fileName = String.Concat(testSection.ClassName.GetClearName(), ".cs");
                    byte[] classContent = ClassBuilder.CreateClass(testSection);

                    projectItem.AddProjectItem(fileDestination, fileName, classContent);
                }
            }

            return Encoding.UTF8.GetBytes(Logger.Message);
        }

        public override string GetDefaultExtension()
        {
            return ".log";
        }
    }
}