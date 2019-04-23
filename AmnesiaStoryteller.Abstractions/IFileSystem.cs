using System.Collections.Generic;

namespace AmnesiaStoryteller.Abstractions
{
    public interface IFileSystem
    {
        IEnumerable<string> GetFiles(string path);
    }
}
