using System;
using System.IO;
using devplex.Tools.Diagnostics;
using EnvDTE;

namespace devplex.Tools.FrameworkExtensions
{
    /// <summary>
    /// Extensions for the ProjectItem class.
    /// </summary>
    internal static class ProjectItemExtensions
    {
        /// <summary>
        /// Adds the project item.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="fileDestination">The file destination.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        internal static void AddProjectItem(
            this ProjectItem instance, 
            string fileDestination, 
            string fileName,
            byte[] content)
        {
            var filePath = Path.Combine(fileDestination, fileName);

            using (var file = File.OpenWrite(filePath))
            {
                file.Write(content, 0, content.Length);
                file.SetLength(content.Length);
            }

            var projectItem = instance.ProjectItems.AddFromFile(filePath);

            if (projectItem != null)
            {
                Logger.Write(
                    string.Concat(
                        fileName, 
                        " has been created successful."));
            }
        }
    }
}