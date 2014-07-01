using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealDocProgram
{
    using RealDocLib;

    class Program
    {
        static void Main(string[] args)
        {
            var docProvider = new MsTestDocumentProvider(args[0]);
            var parser = new DocGenerator(docProvider);
            var documents = parser.Generate();
            foreach (var doc in documents)
            {
                Console.WriteLine(doc.ClassName);
                foreach (var methodName in doc.MethodNames)
                {
                    Console.Write("\t");
                    Console.WriteLine(methodName);
                }
            }
        }
    }
}
