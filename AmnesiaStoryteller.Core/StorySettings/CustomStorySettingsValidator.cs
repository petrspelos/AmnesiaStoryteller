using System.IO;
using System.Linq;
using System.Xml.Linq;
using AmnesiaStoryteller.Abstractions;

namespace AmnesiaStoryteller.Core.StorySettings
{
    public class CustomStorySettingsValidator : ICustomStorySettingsValidator
    {
        private readonly IFileSystem _fileSystem;
        private XDocument _currentDocument;

        public CustomStorySettingsValidator(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public bool StructureIsValid(string filePath)
        {
            try
            {
                var contents = _fileSystem.GetContents(filePath);
                _currentDocument = XDocument.Parse(contents);
                return HasMain() && HasRequiredAttributes();
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        private bool HasMain() => _currentDocument?.Root?.Name == "Main";

        private bool HasRequiredAttributes()
        {
            var attributes = _currentDocument?.Root?.Attributes().ToArray();

            if (attributes is null) { return false; }

            return attributes.Any(a => a.Name == "ImgFile") &&
                   attributes.Any(a => a.Name == "Name") &&
                   attributes.Any(a => a.Name == "Author") &&
                   attributes.Any(a => a.Name == "MapsFolder") &&
                   attributes.Any(a => a.Name == "StartMap") &&
                   attributes.Any(a => a.Name == "StartPos");
        }
    }
}
