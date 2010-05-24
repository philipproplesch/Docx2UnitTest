using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using devplex.Tools.CodeGeneration;
using devplex.Tools.Diagnostics;
using devplex.Tools.FrameworkExtensions;
using devplex.Tools.Model;
using EnvDTE;
using VSLangProj;
using Microsoft.CustomTool;

namespace devplex.Tools
{
    /// <summary>
    /// Docx to UnitTest Code generator
    /// </summary>
    [ComVisible(true)]
    [Guid("3b75d10d-2892-49d9-9939-7d13b56f55ca")]
    public class Docx2UnitTestCodeGenerator
        : BaseCodeGeneratorWithSite
    {
        //TODO: Add project items to foreign project
        //TODO: Generate class documentation
        //TODO: Generate method documentation
        //TODO: xUnit BDD extensions

        #region GenerateCode(string inputFileName, string inputFileContent)
        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <param name="inputFileName">Name of the input file.</param>
        /// <param name="inputFileContent">Content of the input file.</param>
        /// <returns></returns>
        protected override byte[] GenerateCode(
            string inputFileName,
            string inputFileContent)
        {
            var projectItem = GetService(typeof(ProjectItem)) as ProjectItem;

            if (projectItem == null) return Encoding.UTF8.GetBytes("");
            try
            {
                var clrNamespace = "devplex.Samples.Test";

                VSProject vsProject = null;
                Project project = projectItem.ContainingProject;
                if (project != null)
                {
                    vsProject = project.Object as VSProject;

                    var nsProperty =
                        project.Properties.Item("DefaultNamespace");
                    if (nsProperty != null &&
                        !string.IsNullOrEmpty(nsProperty.Value))
                    {
                        clrNamespace = nsProperty.Value;
                    }
                }

                var projectItemPath = projectItem.FileNames[0];
                var fileDestination = Path.GetDirectoryName(inputFileName);

                var testFramework =
                    OpenXmlParser.GetTestFramework(projectItemPath, clrNamespace);

                // Add references to the project.
                AddReferences(vsProject, testFramework);

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
        #endregion

        private void AddReferences(VSProject vsProject, ITestFramework testFramework)
        {
            if (vsProject != null)
            {
                string executingDirectory =
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (testFramework.GetType() == typeof(MsTestFramework))
                {
                    try
                    {
                        vsProject.References.Add(Path.Combine(
                            executingDirectory, "MsTest",
                            "Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll"));
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex.ToString());
                    }
                }
                else if (testFramework.GetType() == typeof(NUnitFramework))
                {
                    try
                    {
                        vsProject.References.Add(Path.Combine(
                            executingDirectory, "NUnit", "nunit.framework.dll"));
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex.ToString());
                    }
                }
                else if (testFramework.GetType() == typeof(XUnitFramework))
                {
                    try
                    {
                        vsProject.References.Add(Path.Combine(
                            executingDirectory, "xUnit.net", "xunit.dll"));
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex.ToString());
                    }
                }
                else if (testFramework.GetType() == typeof(MbUnitFramework))
                {
                    try
                    {
                        vsProject.References.Add(Path.Combine(
                            executingDirectory, "MbUnit", "MbUnit.Framework.dll"));
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex.ToString());
                    }
                }
            }
        }

        #region GetDefaultExtension()
        /// <summary>
        /// Gets the default extension.
        /// </summary>
        /// <returns></returns>
        public override string GetDefaultExtension()
        {
            return ".log";
        }
        #endregion
    }
}