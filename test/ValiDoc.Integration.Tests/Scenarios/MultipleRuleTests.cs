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

	        var ruleGenerator = new DocBuilder(new RuleDescriptionBuilder(new ValidatorErrorMessageBuilder(new FluentValidationHelpers())));

            var validationRules = ruleGenerator.Document(validator).ToList();

            validationRules.Should().HaveCount(4);

            var expectedOutput = new List<RuleDescription>
            {
				new RuleDescription
	            {
		            FailureSeverity = "Error",
		            MemberName = "First Name",
		            OnFailure = "Continue",
		            ValidatorName = "NotNullValidator",
		            ValidationMessage = "'First Name' must not be empty."
	            },
	            new RuleDescription
	            {
		            FailureSeverity = "Error",
		            MemberName = "Last Name",
		            OnFailure = "Continue",
		            ValidatorName = "NotEmptyValidator",
		            ValidationMessage = "'Last Name' should not be empty."
	            },
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Last Name",
                    OnFailure = "Continue",
                    ValidatorName = "MinimumLengthValidator",
                    ValidationMessage = "'Last Name' must be more than {MinLength} characters. You entered {TotalLength} characters."
                },
                new RuleDescription
	            {
		            FailureSeverity = "Error",
		            MemberName = "Last Name",
		            OnFailure = "Continue",
		            ValidatorName = "MaximumLengthValidator",
		            ValidationMessage = "'Last Name' must be less than {MaxLength} characters. You entered {TotalLength} characters."
                }
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
