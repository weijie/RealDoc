namespace RealDocLib
{
    using System;
    using System.Collections.Generic;

    public class Document
    {
        public Document(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentNullException("className");
            }

            this.ClassName = className;
        }

        public string ClassName { set; get; }

        public IList<string> MethodNames { set; get; }
    }
}