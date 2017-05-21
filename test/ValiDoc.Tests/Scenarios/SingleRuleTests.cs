using FluentAssertions;
using ValiDoc.Tests.TestData.Validators;
using Xunit;

namespace ValiDoc.Tests.Scenarios
{
    public class SingleRuleTests
    {
        [Fact]
        public void ValiDoc_WithSingleRuleValidator_OutputsSingleRule()
        {
            var validator = new SingleRuleValidator();

            var validationRules = validator.GetRules();

            validationRules.Should().HaveCount(1);
        }
    }
}
