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
	public class MultipleRuleTests
    {
        [Fact]
        public void ValiDoc_WithMultipleRuleValidator_OutputMultipleRules()
        {
            var validator = new MultipleRuleValidator();
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
                            ValidationMessage = "'First Name' must not be empty.",
                            ValidatorName = "NotNullValidator"
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
                            ValidationMessage = "'Last Name' should not be empty.",
                            ValidatorName = "NotEmptyValidator"
                        },
                        new RuleDescription
                        {
                            FailureSeverity = "Error",
                            OnFailure = "Continue",
                            ValidationMessage = "'Last Name' must be more than {MinLength} characters. You entered {TotalLength} characters.",
                            ValidatorName = "MinimumLengthValidator"
                        },
                        new RuleDescription
                        {
                            FailureSeverity = "Error",
                            OnFailure = "Continue",
                            ValidationMessage = "'Last Name' must be less than {MaxLength} characters. You entered {TotalLength} characters.",
                            ValidatorName = "MaximumLengthValidator"
                        }
                    }
                }
            };

	        var ruleGenerator = new DocBuilder(new RuleDescriptionBuilder(new ValidatorErrorMessageBuilder(new FluentValidationHelpers())));

            var validationRules = ruleGenerator.Document(validator).ToList();

            validationRules.Should().HaveCount(2);
            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
