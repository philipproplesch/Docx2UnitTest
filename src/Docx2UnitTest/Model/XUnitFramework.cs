using System.IO;

namespace devplex.Tools.Model
{
    /// <summary>
    /// Implementation of the test framework from XUnit.
    /// </summary>
    internal class XUnitFramework 
        : BaseTestFramework
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XUnitFramework"/> class.
        /// </summary>
        public XUnitFramework()
        {
            References.Add(Path.Combine("xUnit.net", "xunit.dll"));

            RequiredUsings.Add("using System;");
            RequiredUsings.Add("using Xunit;");

            RequiredMethodAttributes.Add("[Fact]");
        }
    }
}