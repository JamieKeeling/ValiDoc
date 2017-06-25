using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using ValiDoc.Output;
using ValiDoc.Tests.TestData.Validators;
using Xunit;

namespace ValiDoc.Tests.Scenarios
{
    public class MultipleRuleTests
    {
        [Fact]
        public void ValiDoc_WithMultipleRuleValidator_OutputMultipleRules()
        {
            var validator = new MultipleRuleValidator();

            var validationRules = validator.GetRules().ToList();

            validationRules.Should().HaveCount(3);

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
		            ValidatorName = "MaximumLengthValidator",
		            ValidationMessage = "'Last Name' must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} characters."
	            }
			};

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
