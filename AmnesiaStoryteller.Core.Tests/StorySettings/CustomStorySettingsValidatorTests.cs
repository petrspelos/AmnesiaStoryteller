using System.IO;
using AmnesiaStoryteller.Abstractions;
using AmnesiaStoryteller.Core.StorySettings;
using Moq;
using Xunit;

namespace AmnesiaStoryteller.Core.Tests.StorySettings
{
    public class CustomStorySettingsValidatorTests
    {
        private const string TestFilePath = @"C:\myCs\custom_story_settings.cfg";

        private readonly Mock<IFileSystem> _fileSystemMock;
        private readonly ICustomStorySettingsValidator _validator;

        public CustomStorySettingsValidatorTests()
        {
            _fileSystemMock = new Mock<IFileSystem>();
            _validator = new CustomStorySettingsValidator(_fileSystemMock.Object);
        }

        [Fact]
        public void ValidSettingsPass()
        {
            const string contents = @"<Main
    ImgFile = ""thumbnail.jpg""
    Name = ""The Queen of Birds""
    Author = ""Tukashimika and Spelos""

    MapsFolder = ""maps/""
    StartMap = ""TQB_01_house.map""
    StartPos = ""PlayerStartArea_1""
/>";

            _fileSystemMock
                .Setup(fs => fs.GetContents(TestFilePath))
                .Returns(contents);

            Assert.True(_validator.StructureIsValid(TestFilePath));
        }

        [Theory]
        [InlineData("<Main Name=\"\" Author=\"\" MapsFolder=\"\" StartMap=\"\" StartPos=\"\"/>")]
        [InlineData("<Main ImgFile=\"\" Author=\"\" MapsFolder=\"\" StartMap=\"\" StartPos=\"\"/>")]
        [InlineData("<Main ImgFile=\"\" Name=\"\" MapsFolder=\"\" StartMap=\"\" StartPos=\"\"/>")]
        [InlineData("<Main ImgFile=\"\" Name=\"\" Author=\"\" StartMap=\"\" StartPos=\"\"/>")]
        [InlineData("<Main ImgFile=\"\" Name=\"\" Author=\"\" MapsFolder=\"\" StartPos=\"\"/>")]
        [InlineData("<Main ImgFile=\"\" Name=\"\" Author=\"\" MapsFolder=\"\" StartMap=\"\" />")]
        public void MissingAttributeFails(string xml)
        {
            _fileSystemMock
                .Setup(fs => fs.GetContents(TestFilePath))
                .Returns(xml);

            Assert.False(_validator.StructureIsValid(TestFilePath));
        }

        [Fact]
        public void MissingFileFails()
        {
            _fileSystemMock
                .Setup(fs => fs.GetContents(TestFilePath))
                .Returns(() => throw new FileNotFoundException());

            Assert.False(_validator.StructureIsValid(TestFilePath));
        }
    }
}
