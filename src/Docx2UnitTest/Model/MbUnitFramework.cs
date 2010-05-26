using System.IO;

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
            References.Add(Path.Combine("MbUnit", "MbUnit.Framework.dll"));

            RequiredUsings.Add("using System;");
            RequiredUsings.Add("using MbUnit.Framework;");

            RequiredClassAttributes.Add("[TestFixture]");

            RequiredMethodAttributes.Add("[Test]");
        }
    }
}
