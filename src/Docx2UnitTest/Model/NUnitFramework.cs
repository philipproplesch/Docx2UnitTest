namespace Docx2UnitTest.Model
{
    internal class NUnitFramework : BaseTestFramework
    {
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