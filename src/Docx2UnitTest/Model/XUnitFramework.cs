namespace Docx2UnitTest.Model
{
    internal class XUnitFramework : BaseTestFramework
    {
        public XUnitFramework()
        {
            RequiredUsings.Add("using System;");
            RequiredUsings.Add("using Xunit;");


            RequiredMethodAttributes.Add("[Fact]");
        }
    }
}