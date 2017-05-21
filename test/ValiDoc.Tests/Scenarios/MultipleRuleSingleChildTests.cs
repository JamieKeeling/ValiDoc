using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using ValiDoc.Output;
using ValiDoc.Tests.TestData.Validators;
using Xunit;

namespace ValiDoc.Tests.Scenarios
{
    public class MultipleRuleSingleChildTests
    {
        [Fact]
        public void ValiDoc_WithMultipleRuleSingleChildValidator_OutputMultipleRulesAndSingleChild()
        {
            var validator = new MultipleRuleSingleChildValidator();

            var validationRules = validator.GetRules().ToList();

            validationRules.Should().HaveCount(4);

            var expectedOutput = new List<RuleDescription>
            {
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "First Name",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Last Name",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Last Name",
                    OnFailure = "Continue",
                    ValidatorName = "MaximumLengthValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Address",
                    OnFailure = "Continue",
                    ValidatorName = "AddressValidator"
                }
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput);
        }
    }
}
