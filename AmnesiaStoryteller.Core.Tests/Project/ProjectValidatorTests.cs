using System.IO;
using AmnesiaStoryteller.Abstractions;
using AmnesiaStoryteller.Core.Project;
using Moq;
using Xunit;

namespace AmnesiaStoryteller.Core.Tests.Project
{
    public class ProjectValidatorTests
    {
        private readonly Mock<IFileSystem> _fileSystemMock;
        private readonly IProjectValidator _validator;

        public ProjectValidatorTests()
        {
            _fileSystemMock = new Mock<IFileSystem>();
            _validator = new ProjectValidator(_fileSystemMock.Object);
        }

        [Theory]
        [InlineData("extra_french.lang")]
        [InlineData("extra_german.lang")]
        [InlineData("extra_english.lang")]
        [InlineData("extra_chinese.lang")]
        [InlineData("extra_italian.lang")]
        [InlineData("extra_russian.lang")]
        [InlineData("extra_spanish.lang")]
        [InlineData("extra_brazilian_portuguese.lang")]
        public void ValidCustomStoryPasses(string validLangFile)
        {
            const string path = @"C:\TestDirectory\MyCustomStory";

            _fileSystemMock
                .Setup(fs => fs.GetFiles(path))
                .Returns(new [] { "custom_story_settings.cfg", "thumbnail.png", validLangFile });

            Assert.True(_validator.DirectoryContainsCustomStory(path));
        }

        [Fact]
        public void MissingSettingsFails()
        {
            const string path = @"C:\TestDirectory\MyCoolStory";

            _fileSystemMock
                .Setup(fs => fs.GetFiles(path))
                .Returns(new[] { "extra_english.lang" });

            Assert.False(_validator.DirectoryContainsCustomStory(path));
        }

        [Fact]
        public void MissingLangFails()
        {
            const string path = @"C:\TestDirectory\Story";

            _fileSystemMock
                .Setup(fs => fs.GetFiles(path))
                .Returns(new[] { "custom_story_settings.cfg", "thumbnail.png" });

            Assert.False(_validator.DirectoryContainsCustomStory(path));
        }

        [Fact]
        public void EmptyDirectoryFails()
        {
            const string path = @"C:\TestDirectory\EmptyDir";

            _fileSystemMock
                .Setup(fs => fs.GetFiles(path))
                .Returns(new string[] { });

            Assert.False(_validator.DirectoryContainsCustomStory(path));
        }

        [Fact]
        public void DirectoryNotFoundFails()
        {
            const string path = @"C:\TestDirectory\FakeDir";

            _fileSystemMock
                .Setup(fs => fs.GetFiles(path))
                .Returns(() => throw new DirectoryNotFoundException());
            
            Assert.False(_validator.DirectoryContainsCustomStory(path));
        }
    }
}
