using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
<<<<<<< HEAD
using BetterCode.Tools;
using Docx2UnitTest.CodeGeneration;
using Docx2UnitTest.Diagnostics;
=======
using Docx2UnitTest.Common;
>>>>>>> f27f69da501f20d5e3bc428b8b321847e4c37fab
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

                var testSections = 
                    OpenXmlParser.GetTestFramework(projectItemPath);



                foreach (var testClass in testSections.Classes)
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