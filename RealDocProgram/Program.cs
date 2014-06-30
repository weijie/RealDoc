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
            var parser = new TestMethodNameParser(args[0]);
            var dictionary = parser.Parse();
            foreach (var className in dictionary)
            {
                Console.WriteLine(className.Key);
                foreach (var methodName in className.Value)
                {
                    Console.Write("\t");
                    Console.WriteLine(methodName);
                }
            }
        }
    }
}
