namespace devplex.Tools.Model
{
    /// <summary>
    /// Implementation of the test framework from Microsoft.
    /// </summary>
    internal class MsTestFramework 
        : BaseTestFramework
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsTestFramework"/> class.
        /// </summary>
        public MsTestFramework()
        {
            RequiredUsings.Add("using System;");
            RequiredUsings.Add("using Microsoft.VisualStudio.TestTools.UnitTesting;");

            RequiredClassAttributes.Add("[TestClass]");

            RequiredMethodAttributes.Add("[TestMethod]");
        }
    }
}