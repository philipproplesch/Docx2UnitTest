using System.Collections.Generic;

namespace devplex.Tools.Model
{
    /// <summary>
    /// Implementation definition for a test framework
    /// </summary>
    internal interface ITestFramework
    {
        /// <summary>
        /// Gets the required usings.
        /// </summary>
        /// <value>The required usings.</value>
        List<string> RequiredUsings { get; }

        /// <summary>
        /// Gets the required class attributes.
        /// </summary>
        /// <value>The required class attributes.</value>
        List<string> RequiredClassAttributes { get; }

        /// <summary>
        /// Gets the required method attributes.
        /// </summary>
        /// <value>The required method attributes.</value>
        List<string> RequiredMethodAttributes { get; }

        /// <summary>
        /// Gets the classes.
        /// </summary>
        /// <value>The classes.</value>
        List<TestClassModel> Classes { get; }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the target project.
        /// </summary>
        /// <value>The target project.</value>
        string TargetProject { get; set; }


        /// <summary>
        /// Creates the test class model.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        TestClassModel CreateTestClassModel(string name);

        /// <summary>
        /// Creates the test model.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        TestModel CreateTestModel(string name);

    }
}