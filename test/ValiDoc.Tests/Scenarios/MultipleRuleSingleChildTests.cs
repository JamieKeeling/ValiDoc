using FluentAssertions;
using ValiDoc.Tests.TestData.Validators;
using Xunit;

namespace ValiDoc.Tests.Scenarios
{
    public class MultipleRuleSingleChildTests
    {
        [Fact]
        public void ValiDoc_WithMultipleRuleSingleChildValidator_OutputsMultipleRulesAndSingleChild()
        {
            var validator = new MultipleRuleSingleChildValidator();

            var validationRules = validator.GetRules();

            validationRules.Should().HaveCount(4);
        }
    }
}
