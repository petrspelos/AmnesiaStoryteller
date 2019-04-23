using AmnesiaStoryteller.Core.Project;
using Xunit;

namespace AmnesiaStoryteller.Core.Tests.Project
{
    public class ProjectValidatorTests
    {
        private ProjectValidator _validator;

        public ProjectValidatorTests()
        {
            _validator = new ProjectValidator();
        }

        [Fact]
        public void ValidCustomStoryPasses()
        {

        }
    }
}
