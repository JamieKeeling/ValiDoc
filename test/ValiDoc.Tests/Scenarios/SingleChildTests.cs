using System.Linq;
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

            var validationRules = validator.GetRules().ToList();

            validationRules.Should().HaveCount(1);

            validationRules.Should().NotContain(rule => string.IsNullOrEmpty(rule.MemberName)
                                                        && string.IsNullOrEmpty(rule.ValidatorName)
                                                        && string.IsNullOrEmpty(rule.FailureSeverity));
        }
    }
}
