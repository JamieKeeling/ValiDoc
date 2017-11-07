using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using ValiDoc.CommonTest.TestData.Validators;
using ValiDoc.Core;
using ValiDoc.Output;
using ValiDoc.Utility;
using Xunit;

namespace ValiDoc.Integration.Tests.Scenarios
{
    public class SingleChildTests
    {
        [Fact]
        public void ValiDoc_WithSingleChildValidator_OutputSingleRule()
        {
            var validator = new SingleChildValidator();

			var ruleGenerator = new DocBuilder(new RuleDescriptionBuilder(new ValidatorErrorMessageBuilder(new FluentValidationHelpers())));

	        var validationRules = ruleGenerator.Document(validator).ToList();

			var expectedOutput = new List<RuleDescriptor>
            {
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

            validationRules.Should().HaveCount(1);
            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
