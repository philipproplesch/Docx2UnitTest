using System;
using System.Collections.Generic;
using System.Text;

namespace devplex.Tools.Model
{
    /// <summary>
    /// BAse implementatiopn for a test framework.
    /// </summary>
    internal class BaseTestFramework
        : ITestFramework
    {


        #region TargetProject
        /// <summary>
        /// Gets or sets the target project.
        /// </summary>
        /// <value>The target project.</value>
        public string TargetProject { get; set; }
        #endregion

        #region Namespace
        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public string Namespace { get; set; }
        #endregion

        #region References
        /// <summary>
        /// Gets or sets the references.
        /// </summary>
        /// <value>The references.</value>
        public List<string> References { get; protected set; }
        #endregion

        #region RequiredUsings
        /// <summary>
        /// Gets or sets the required usings.
        /// </summary>
        /// <value>The required usings.</value>
        public List<string> RequiredUsings { get; protected set; }
        #endregion

        #region RequiredClassAttributes
        /// <summary>
        /// Gets or sets the required class attributes.
        /// </summary>
        /// <value>The required class attributes.</value>
        public List<string> RequiredClassAttributes { get; set; }
        #endregion

        #region RequiredMethodAttributes
        /// <summary>
        /// Gets or sets the required method attributes.
        /// </summary>
        /// <value>The required method attributes.</value>
        public List<string> RequiredMethodAttributes { get; set; }
        #endregion

        #region Classes
        /// <summary>
        /// Gets or sets the classes.
        /// </summary>
        /// <value>The classes.</value>
        public List<TestClassModel> Classes { get; private set; }
        #endregion


        #region ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestFramework"/> class.
        /// </summary>
        public BaseTestFramework()
        {
            Classes = new List<TestClassModel>();
            References = new List<string>();
            RequiredUsings = new List<string>();
            RequiredClassAttributes = new List<string>();
            RequiredMethodAttributes = new List<string>();
        }
        #endregion


        #region CreateTestClassModel(string name)
        /// <summary>
        /// Creates the test class model.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public TestClassModel CreateTestClassModel(string name)
        {
            var testClass =
                new TestClassModel
                    {
                        Name = name,
                        Namespace = Namespace,
                        UsingStatements = RequiredUsings
                    };

            var classAttributeBuilder = new StringBuilder();
            var counter = 0;
            foreach (var attribute in RequiredClassAttributes)
            {
                if (counter > 0) classAttributeBuilder.Append("\t");
                classAttributeBuilder.Append(attribute);
                classAttributeBuilder.Append("\r\n");
                counter++;
            }
            if (classAttributeBuilder.Length > 0)
            {
                classAttributeBuilder.Remove(
                    classAttributeBuilder.Length - 2, 2);
            }
            testClass.Attributes = classAttributeBuilder.ToString();

            return testClass;

        }
        #endregion

        #region CreateTestModel(string name)
        /// <summary>
        /// Creates the test model.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
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
            var counter = 0;
            foreach (var attribute in RequiredMethodAttributes)
            {
                if (counter > 0) attributeBuilder.Append("\t\t");
                attributeBuilder.Append(attribute);
                attributeBuilder.Append("\r\n");
                counter++;
            }
            if (attributeBuilder.Length > 0)
            {
                attributeBuilder.Remove(
                    attributeBuilder.Length - 2, 2);
            }
            test.Attributes = attributeBuilder.ToString();

            return test;

        }
        #endregion

    }
}