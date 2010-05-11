namespace Docx2UnitTest.Model
{
    internal class MsTestFramework : BaseTestFramework
    {
        public MsTestFramework()
        {
            RequiredUsings.Add("using System;");
            RequiredUsings.Add("using Microsoft.VisualStudio.TestTools.UnitTesting;");

            RequiredClassAttributes.Add("[TestClass]");

            RequiredMethodAttributes.Add("[TestMethod]");
        }
    }
}