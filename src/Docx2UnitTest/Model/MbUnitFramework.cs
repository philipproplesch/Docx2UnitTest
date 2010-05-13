namespace devplex.Tools.Model
{
    /// <summary>
    /// Implementation of the test framework from NUnit.
    /// </summary>
    internal class MbUnitFramework
        : BaseTestFramework
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NUnitFramework"/> class.
        /// </summary>
        public MbUnitFramework()
        {
            RequiredUsings.Add("using System;");
            RequiredUsings.Add("using MbUnit.Framework;");

            RequiredClassAttributes.Add("[TestFixture]");

            RequiredMethodAttributes.Add("[Test]");
        }
    }
}
