using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using ValiDoc.Output;
using ValiDoc.Tests.TestData.Validators;
using Xunit;

namespace ValiDoc.Tests.Scenarios
{
    public class DeepDocumentTests
    {
        [Fact]
        public void ValiDoc_WithMultipleRuleSingleChildAndDeepDocument_ReturnsRulesForAllIncludingChildValidator()
        {
            var validator = new MultipleRuleSingleChildValidator();

            var validationRules = validator.GetRules(true).ToList();

            validationRules.Should().HaveCount(7);

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
                    MemberName = "House Number",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Street Name",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Post Code",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Address",
                    OnFailure = "Continue",
                    ValidatorName = "AddressValidator"
                }
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }

        [Fact]
        public void ValiDoc_WithNoChildValidatorAndDeepDocument_ReturnsRulesForAll()
        {
            var validator = new MultipleRuleValidator();

            var validationRules = validator.GetRules(true).ToList();

            validationRules.Should().HaveCount(3);

            var expectedOutput = new List<RuleDescription>
            {
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "First Name",
                    OnFailure = "Continue",
                    ValidatorName = "NotNullValidator"
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
                }
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());

        }

        [Fact]
        public void ValiDoc_WithMultipleChildValidatorsAndDeepDocument_ReturnsRulesForAllChildValidators()
        {
            var validator = new MultipleRuleMultipleChildValidator();

            var validationRules = validator.GetRules(true).ToList();

            validationRules.Should().HaveCount(11);

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
                    MemberName = "House Number",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Street Name",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Post Code",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Address",
                    OnFailure = "Continue",
                    ValidatorName = "AddressValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Employment Status",
                    OnFailure = "Continue",
                    ValidatorName = "NotEqualValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Employment Status",
                    OnFailure = "Continue",
                    ValidatorName = "EnumValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Job Title",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator"
                },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Occupation Details",
                    OnFailure = "Continue",
                    ValidatorName = "OccupationDetailsValidator"
                },
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
