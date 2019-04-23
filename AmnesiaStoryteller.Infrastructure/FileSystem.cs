using System.Collections.Generic;
using System.IO;
using AmnesiaStoryteller.Abstractions;

namespace AmnesiaStoryteller.Infrastructure
{
    public class FileSystem : IFileSystem
    {
        public IEnumerable<string> GetFiles(string path) => Directory.GetFiles(path);
    }
}
