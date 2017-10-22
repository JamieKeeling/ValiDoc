using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ValiDoc.CommonTest.TestData.Validators;
using ValiDoc.Core;
using ValiDoc.Output;
using ValiDoc.Utility;
using Xunit;

namespace ValiDoc.Integration.Tests.Scenarios
{
	public class CascadeModeTests
    {
        [Fact]
        public void ValiDoc_WithMultipleRuleWithGlobalCascadeValidator_OutputMultipleRulesWithIdenticalCascade()
        {
            var validator = new MultipleRuleValidatorWithGlobalCascade();

	        var ruleGenerator = new DocBuilder(new RuleDescriptionBuilder(new ValidatorErrorMessageBuilder(new FluentValidationHelpers())));

            var validationRules = ruleGenerator.Document(validator).ToList();

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
	                ValidationMessage = "'Last Name' must be less than {MaxLength} characters. You entered {TotalLength} characters."
				}
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }

        [Fact]
        public void ValiDoc_WithGlobalCascadeValidatorAndRuleOverride_OutputMultipleRulesWithPOverriddenCascade()
        {
            var validator = new MultipleRuleValidatorWithMixedCascade();

			var ruleGenerator = new DocBuilder(new RuleDescriptionBuilder(new ValidatorErrorMessageBuilder(new FluentValidationHelpers())));

	        var validationRules = ruleGenerator.Document(validator).ToList();

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
					ValidationMessage = "'Last Name' must be less than {MaxLength} characters. You entered {TotalLength} characters."

                }
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
