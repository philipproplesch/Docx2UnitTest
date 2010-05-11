using System.Collections.Generic;

namespace Docx2UnitTest.TestFrameworks
{
    internal class XUnitFramework : ITestFramework
    {
        #region ITestFramework Members

        public List<string> RequiredUsings
        {
            get
            {
                return new List<string>
                           {"using System;", "using Xunit;"};
            }
        }

        public List<string> ClassAttributes
        {
            get { return new List<string>(); }
        }

        public List<string> MethodAttributes
        {
            get { return new List<string> {"[Fact]"}; }
        }

        #endregion
    }
}