using System.Collections.Generic;

namespace devplex.Tools.Model
{
    /// <summary>
    /// A model of a test class.
    /// </summary>
    internal class TestClassModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestClassModel"/> class.
        /// </summary>
        public TestClassModel()
        {
            UsingStatements = new List<string>();
            Tests = new List<TestModel>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the using statements.
        /// </summary>
        /// <value>The using statements.</value>
        public List<string> UsingStatements { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>The attributes.</value>
        public string Attributes { get; set; }

        /// <summary>
        /// Gets or sets the tests.
        /// </summary>
        /// <value>The tests.</value>
        public List<TestModel> Tests { get; set; }
    }
}