using FluentAssertions;
using ValiDoc.Tests.TestData.Validators;
using Xunit;

namespace ValiDoc.Tests.Scenarios
{
    public class MultipleRuleTests
    {
        [Fact]
        public void ValiDoc_WithMultipleRuleValidator_OutputsMultipleRules()
        {
            var validator = new MultipleRuleValidator();

            var validationRules = validator.GetRules();

            validationRules.Should().HaveCount(3);
        }
    }
}
