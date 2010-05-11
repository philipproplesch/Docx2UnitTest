using System;
using System.IO;
using Docx2UnitTest.Diagnostics;
using EnvDTE;

namespace Docx2UnitTest.FrameworkExtensions
{
    internal static class ProjectItemExtensions
    {
        internal static void AddProjectItem(this ProjectItem instance, string fileDestination, string fileName,
                                            byte[] content)
        {
            string filePath = Path.Combine(fileDestination, fileName);

            //if (File.Exists(filePath))
            //    return;

            using (FileStream file = File.OpenWrite(filePath))
            {
                file.Write(content, 0, content.Length);
                file.SetLength(content.Length);
            }

            ProjectItem projectItem = instance.ProjectItems.AddFromFile(filePath);

            if (projectItem != null)
                Logger.Write(String.Concat(fileName, " has been created successful."));
        }
    }
}