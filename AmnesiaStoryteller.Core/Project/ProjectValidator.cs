using System.IO;
using System.Linq;
using AmnesiaStoryteller.Abstractions;

namespace AmnesiaStoryteller.Core.Project
{
    public class ProjectValidator : IProjectValidator
    {
        private readonly IFileSystem _fileSystem;

        public ProjectValidator(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public bool DirectoryContainsCustomStory(string path)
        {
            var files = _fileSystem.GetFiles(path).Select(Path.GetFileName).ToArray();
            return files.Any(FileIsBaseLang) && files.Any(f => f == "custom_story_settings.cfg");
        }

        private static bool FileIsBaseLang(string fileName) 
            => Constants.BaseLanguages.Any(l => fileName == $"extra_{l}.lang");
    }
}
