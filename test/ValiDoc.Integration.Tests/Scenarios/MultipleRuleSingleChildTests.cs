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
    public class MultipleRuleSingleChildTests
    {
        [Fact]
        public void ValiDoc_WithMultipleRuleSingleChildValidator_OutputMultipleRulesAndSingleChild()
        {
            var validator = new MultipleRuleSingleChildValidator();
            var expectedOutput = new List<RuleDescriptor>
            {
                new RuleDescriptor
                {
                    MemberName = "First Name",
                    Rules = new List<RuleDescription>
                    {
                        new RuleDescription
                        {
                            FailureSeverity = "Error",
                            OnFailure = "Continue",
                            ValidatorName = "NotEmptyValidator",
                            ValidationMessage = "'First Name' should not be empty."
                        }
                    }
                },
                new RuleDescriptor
                {
                    MemberName = "Last Name",
                    Rules = new List<RuleDescription>
                    {
                        new RuleDescription
                        {
                            FailureSeverity = "Error",
                            OnFailure = "Continue",
                            ValidatorName = "NotEmptyValidator",
                            ValidationMessage = "'Last Name' should not be empty."
                        },
                        new RuleDescription
                        {
                            FailureSeverity = "Error",
                            OnFailure = "Continue",
                            ValidatorName = "MaximumLengthValidator",
                            ValidationMessage = "'Last Name' must be less than {MaxLength} characters. You entered {TotalLength} characters."
                        }
                    }
                },
                new RuleDescriptor
                {
                    MemberName = "Address",
                    Rules = new List<RuleDescription>
                    {
                        new RuleDescription
                        {
                            FailureSeverity = "Error",
                            OnFailure = "Continue",
                            ValidatorName = "AddressValidator",
                            ValidationMessage = "N/A - Refer to specific AddressValidator documentation"
                        }
                    }
                }
            };

	        var ruleGenerator = new DocBuilder(new RuleDescriptionBuilder(new ValidatorErrorMessageBuilder(new FluentValidationHelpers())));

            var validationRules = ruleGenerator.Document(validator).ToList();

            validationRules.Should().HaveCount(3);
            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
