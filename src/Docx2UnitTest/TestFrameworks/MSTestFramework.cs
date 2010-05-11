using System.Collections.Generic;

namespace Docx2UnitTest.TestFrameworks
{
    internal class MSTestFramework : ITestFramework
    {
        #region ITestFramework Members

        public List<string> RequiredUsings
        {
            get { return new List<string> {"using System;", "using Microsoft.VisualStudio.TestTools.UnitTesting;"}; }
        }

        public List<string> ClassAttributes
        {
            get { return new List<string> {"[TestClass]"}; }
        }

        public List<string> MethodAttributes
        {
            get { return new List<string> {"[TestMethod]"}; }
        }

        #endregion
    }
}