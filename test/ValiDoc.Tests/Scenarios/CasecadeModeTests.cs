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

	        var ruleGenerator = new ValiDoc();

            var validationRules = ruleGenerator.GetRules(validator).ToList();

            var expectedOutput = new List<RuleDescription>
            {
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "First Name",
                    OnFailure = "StopOnFirstFailure",
                    ValidatorName = "NotEmptyValidator",
	                ValidationMessage = "'First Name' should not be empty."
				},
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Last Name",
                    OnFailure = "StopOnFirstFailure",
                    ValidatorName = "NotEmptyValidator",
	                ValidationMessage = "'Last Name' should not be empty."
				},
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Last Name",
                    OnFailure = "StopOnFirstFailure",
                    ValidatorName = "MaximumLengthValidator",
	                ValidationMessage = "'Last Name' must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} characters."
				}
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }

        [Fact]
        public void ValiDoc_WithGlobalCascadeValidatorAndRuleOverride_OutputMultipleRulesWithPOverriddenCascade()
        {
            var validator = new MultipleRuleValidatorWithMixedCascade();

			var ruleGenerator = new ValiDoc();

	        var validationRules = ruleGenerator.GetRules(validator).ToList();

			var expectedOutput = new List<RuleDescription>
            {
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "First Name",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator",
					ValidationMessage = "'First Name' should not be empty."
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Last Name",
                    OnFailure = "StopOnFirstFailure",
                    ValidatorName = "NotEmptyValidator",
					ValidationMessage = "'Last Name' should not be empty."
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Last Name",
                    OnFailure = "StopOnFirstFailure",
                    ValidatorName = "MaximumLengthValidator",
					ValidationMessage = "'Last Name' must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} characters."

				}
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
