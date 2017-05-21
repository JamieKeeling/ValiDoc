using System.Linq;
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

            var validationRules = validator.GetRules().ToList();

            validationRules.Should().HaveCount(4);

            validationRules.Should().NotContain(rule => string.IsNullOrEmpty(rule.MemberName)
                                                        && string.IsNullOrEmpty(rule.ValidatorName)
                                                        && string.IsNullOrEmpty(rule.FailureSeverity));
        }
    }
}
