namespace RealDocLib
{
    using System.Collections.Generic;

    public interface IDocumentProvider
    {
        IEnumerable<string> GetClassNames();

        IList<string> GetMethodNames(string className);
    }
}