using System.Collections.Generic;

namespace Docx2UnitTest.Model
{
    internal interface ITestFramework
    {
        List<string> RequiredUsings { get; }

        List<string> RequiredClassAttributes { get; }

        List<string> RequiredMethodAttributes { get; }

        List<TestClassModel> Classes { get; }

        TestClassModel CreateTestClassModel(string name);

        TestModel CreateTestModel(string name);
    }
}