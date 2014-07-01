namespace RealDocLib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class MsTestDocumentProvider : IDocumentProvider
    {
        private const string TestClassAttribute = "TestClassAttribute";

        private const string TestMethodAttribute = "TestMethodAttribute";

        private readonly Assembly assembly;

        public MsTestDocumentProvider(string assemblyFilePath)
        {
            if (string.IsNullOrWhiteSpace(assemblyFilePath))
            {
                throw new ArgumentException("assemblyFilePath");
            }

            if (!File.Exists(assemblyFilePath))
            {
                throw new ArgumentException("Assembly file does not exist: " + assemblyFilePath);
            }

            this.assembly = Assembly.LoadFile(assemblyFilePath);

        }

        public IEnumerable<string> GetClassNames()
        {
            var classNames = new List<string>();
            foreach (var classType in this.assembly.GetTypes())
            {
                var customAttributes = classType.GetCustomAttributes(true);
                classNames.AddRange(from customAttribute in customAttributes where customAttribute.GetType().Name == TestClassAttribute select classType.FullName);
            }

            return classNames;
        }

        public IList<string> GetMethodNames(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentException("className");
            }
            
            var names = new List<string>();
            var classType = this.assembly.GetType(className);
            if (classType != null)
            {
                foreach (var methodInfo in classType.GetMethods())
                {
                    var attributes = methodInfo.GetCustomAttributes();
                    if (attributes != null)
                    {
                        var testMethodAttribute = attributes.FirstOrDefault(attribute => attribute.GetType().Name == TestMethodAttribute);
                        if (testMethodAttribute != null)
                        {
                            names.Add(methodInfo.Name);
                        }
                    }
                }
            }

            return names;
        }
    }
}