using System.Collections.Generic;

namespace Docx2UnitTest.TestFrameworks
{
    internal class NUnitFramework : ITestFramework
    {
        #region ITestFramework Members

        public List<string> RequiredUsings
        {
            get
            {
                return new List<string>
                           {"System;", "NUnit.Framework"};
            }
        }

        public List<string> ClassAttributes
        {
            get { return new List<string> {"[TestFixture]", "[Category(\"\")]"}; }
        }

        public List<string> MethodAttributes
        {
            get { return new List<string> {"[Test]"}; }
        }

        #endregion
    }
}