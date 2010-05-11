using System.Collections.Generic;

namespace Docx2UnitTest.TestFrameworks
{
    public interface ITestFramework
    {
        List<string> RequiredUsings { get; }
        List<string> ClassAttributes { get; }
        List<string> MethodAttributes { get; }
    }
}