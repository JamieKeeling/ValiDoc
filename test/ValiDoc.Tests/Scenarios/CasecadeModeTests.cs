using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using ValiDoc.Output;
using ValiDoc.Tests.TestData.Validators;
using Xunit;

namespace ValiDoc.Tests.Scenarios
{
    public class CasecadeModeTests
    {
        [Fact]
        public void ValiDoc_WithMultipleRuleWithGlobalCascadeValidator_OutputMultipleRulesWithIdenticalCascade()
        {
            var validator = new MultipleRuleValidatorWithGlobalCascade();

            var validationRules = validator.GetRules().ToList();

            var expectedOutput = new List<RuleDescription>
            {
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "First Name",
                    OnFailure = "StopOnFirstFailure",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Last Name",
                    OnFailure = "StopOnFirstFailure",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Last Name",
                    OnFailure = "StopOnFirstFailure",
                    ValidatorName = "MaximumLengthValidator"
                }
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }

        [Fact]
        public void ValiDoc_WithGlobalCascadeValidatorAndRuleOverride_OutputMultipleRulesWithPOverriddenCascade()
        {
            var validator = new MultipleRuleValidatorWithMixedCascade();

            var validationRules = validator.GetRules().ToList();

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
                    OnFailure = "StopOnFirstFailure",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Last Name",
                    OnFailure = "StopOnFirstFailure",
                    ValidatorName = "MaximumLengthValidator"
                }
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
