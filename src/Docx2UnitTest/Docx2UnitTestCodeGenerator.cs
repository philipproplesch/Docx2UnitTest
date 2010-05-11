using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Docx2UnitTest.Common;
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
            try
            {
                var projectItem = GetService(typeof(ProjectItem)) as ProjectItem;

                if (projectItem != null)
                {
                    var code = ExistingCodeParser.GetExistingCode(projectItem.ProjectItems);

                    foreach (var file in code.Keys)
                    {
                        Logger.Write(file);
                        var fileContent = code[file];
                        foreach (var method in fileContent.Keys)
                        {
                            Logger.Write(method);
                            Logger.Write(fileContent[method]);
                            Logger.Write(string.Empty);
                        }
                    }

                    var projectItemPath = projectItem.FileNames[0];
                    var fileDestination = Path.GetDirectoryName(inputFileName);

                    var testSections = WordDocumentHelper.GetTestSections(projectItemPath);

                    foreach (var testSection in testSections)
                    {
                        var fileName = string.Concat(testSection.ClassName.GetClearName(), ".cs");
                        var classContent = ClassBuilder.CreateClass(testSection);

                        projectItem.AddProjectItem(fileDestination, fileName, classContent);
                    }
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