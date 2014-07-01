// ------------------------------------------------------------------------------------------------
// <copyright file="TestMethodNameParser.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace RealDocLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class TestMethodNameParser
    {
        private readonly string dllFullPath;

        public TestMethodNameParser(string dllFullPath)
        {
            if (dllFullPath == null)
            {
                throw new ArgumentNullException("dllFullPath");
            }

            this.dllFullPath = dllFullPath;
        }

        public IDictionary<string, IList<string>> Parse()
        {
            var assembly = Assembly.LoadFile(this.dllFullPath);
            var testMethods = new Dictionary<string, IList<string>>();
            foreach (var classType in assembly.GetTypes())
            {
                var customAttributes = classType.GetCustomAttributes(true);
                foreach (var customAttribute in customAttributes)
                {
                    if (customAttribute.GetType().Name == "TestClassAttribute")
                    {
                        var nameList = new List<string>();
                        testMethods.Add(classType.FullName, nameList);
                        foreach (var methodInfo in classType.GetMethods())
                        {
                            var attributes = methodInfo.GetCustomAttributes();
                            if (attributes != null)
                            {
                                var testMethodAttribute = attributes.FirstOrDefault(attribute => attribute.GetType().Name == "TestMethodAttribute");
                                if (testMethodAttribute != null)
                                {
                                    nameList.Add(methodInfo.Name);
                                }
                            }
                        }
                    }
                }
            }

            return testMethods;
        }
    }
}