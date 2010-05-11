using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using BetterCode.Tools;
using Docx2UnitTest.CodeGeneration;
using Docx2UnitTest.Diagnostics;
using Docx2UnitTest.FrameworkExtensions;
using EnvDTE;
using Microsoft.CustomTool;

namespace Docx2UnitTest
{
    [ComVisible(true)]
    [Guid("3b75d10d-2892-49d9-9939-7d13b56f55ca")]
    public class Docx2UnitTestCodeGenerator : BaseCodeGeneratorWithSite
    {
        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {

            var projectItem = GetService(typeof(ProjectItem)) as ProjectItem;

            if (projectItem == null) return Encoding.UTF8.GetBytes("");
            try
            {
                var projectItemPath = projectItem.FileNames[0];
                var fileDestination = Path.GetDirectoryName(inputFileName);

                var testFramework = 
                    OpenXmlParser.GetTestFramework(projectItemPath);

                TestFileModelParser.UpdateModel(
                    projectItem.ProjectItems, 
                    testFramework);

                foreach (var testClass in testFramework.Classes)
                {
                    var fileName = string.Concat(testClass.Name, ".cs");
                    var classContent = ClassBuilder.CreateClass(testClass);
                    projectItem.AddProjectItem(
                        fileDestination, 
                        fileName,
                        classContent);
                }

            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }
            return Encoding.UTF8.GetBytes(Logger.Message);
        }

        public override string GetDefaultExtension()
        {
            return ".log";
        }
    }
}