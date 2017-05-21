using FluentAssertions;
using ValiDoc.Tests.TestData.Validators;
using Xunit;

namespace ValiDoc.Tests.Scenarios
{
    public class SingleChildTests
    {
        [Fact]
        public void ValiDoc_WithSingleChildValidator_OutputsSingleRule()
        {
            var validator = new SingleChildValidator();

            var validationRules = validator.GetRules();

            validationRules.Should().HaveCount(1);
        }
    }
}
