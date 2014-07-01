namespace RealDocLib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class DocGenerator
    {
        private readonly MsTestDocumentProvider provider;

        public DocGenerator(MsTestDocumentProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            this.provider = provider;
        }

        public IEnumerable<Document> Generate()
        {
            var docs = new List<Document>();
            var classNames = this.provider.GetClassNames();
            foreach (var className in classNames)
            {
                var doc = new Document(className);
                var methodNames = this.provider.GetMethodNames(className);
                doc.MethodNames = methodNames;
                docs.Add(doc);
            }

            return docs;
        } 
    }
}