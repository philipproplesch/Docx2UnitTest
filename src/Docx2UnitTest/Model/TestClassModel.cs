using System.Collections.Generic;

namespace Docx2UnitTest.Model
{
    internal class TestClassModel
    {
        public TestClassModel()
        {
            UsingStatements = new List<string>();
            Tests = new List<TestModel>();
        }

        public string Name { get; set; }
        
        public List<string> UsingStatements { get; set; }

        public string Attributes { get; set; }

        public List<TestModel> Tests { get; set; }
    }
}