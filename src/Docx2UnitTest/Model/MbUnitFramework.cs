namespace devplex.Tools.Model
{
    /// <summary>
    /// Implementation of the test framework from MbUnit.
    /// </summary>
    internal class MbUnitFramework
        : BaseTestFramework
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MbUnitFramework"/> class.
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
