using System.Collections.Generic;
using System.Text;

namespace Docx2UnitTest.Model
{
    internal class BaseTestFramework : ITestFramework
    {
        public BaseTestFramework()
        {
            Classes = new List<TestClassModel>();
            RequiredUsings = new List<string>();
            RequiredClassAttributes = new List<string>();
            RequiredMethodAttributes = new List<string>();
        }

        public List<string> RequiredUsings { get; protected set; }
        public List<string> RequiredClassAttributes { get; set; }
        public List<string> RequiredMethodAttributes { get; set; }
        public List<TestClassModel> Classes { get; private set; }

        public TestClassModel CreateTestClassModel(string name)
        {
            var testClass =
                new TestClassModel
                    {
                        Name = name,
                        UsingStatements = RequiredUsings
                    };

            var classAttributeBuilder = new StringBuilder();
            foreach (var attribute in RequiredClassAttributes)
            {
                classAttributeBuilder.Append("\t\t");
                classAttributeBuilder.Append(attribute);
                classAttributeBuilder.Append("\r\n");
            }
            testClass.Attributes = classAttributeBuilder.ToString();

            return testClass;

        }

        public TestModel CreateTestModel(string name)
        {
            var test =
                new TestModel
                    {
                        Name = name,
                        Implementation = 
                        "\r\n\t\t\tthrow new NotImplementedException();",
                };


            var attributeBuilder = new StringBuilder();
            foreach (var attribute in RequiredMethodAttributes)
            {
                attributeBuilder.Append("\t\t");
                attributeBuilder.Append(attribute);
                attributeBuilder.Append("\r\n");
            }
            test.Attributes = attributeBuilder.ToString();

            return test;

        }

    }
}