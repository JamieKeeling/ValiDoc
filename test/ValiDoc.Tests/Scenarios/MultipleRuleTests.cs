using System.Linq;
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

            var validationRules = validator.GetRules().ToList();

            validationRules.Should().HaveCount(3);

            validationRules.Should().NotContain(rule => string.IsNullOrEmpty(rule.MemberName)
                                                        && string.IsNullOrEmpty(rule.ValidatorName)
                                                        && string.IsNullOrEmpty(rule.FailureSeverity));
        }
    }
}
