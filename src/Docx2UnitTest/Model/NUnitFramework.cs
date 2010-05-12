namespace devplex.Tools.Model
{
    /// <summary>
    /// Implementation of the test framework from NUnit.
    /// </summary>
    internal class NUnitFramework 
        : BaseTestFramework
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NUnitFramework"/> class.
        /// </summary>
        public NUnitFramework()
        {
            RequiredUsings.Add("using System;");
            RequiredUsings.Add("using NUnit.Framework;");

            RequiredClassAttributes.Add("[TestFixture]");
            RequiredClassAttributes.Add("[Category(\"\")]");

            RequiredMethodAttributes.Add("[Test]");
        }
    }
}