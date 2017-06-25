using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using ValiDoc.Output;
using ValiDoc.Tests.TestData.Validators;
using Xunit;

namespace ValiDoc.Tests.Scenarios
{
    public class SingleChildTests
    {
        [Fact]
        public void ValiDoc_WithSingleChildValidator_OutputSingleRule()
        {
            var validator = new SingleChildValidator();

			var ruleGenerator = new DocBuilder();

	        var validationRules = ruleGenerator.Document(validator).ToList();

			var expectedOutput = new List<RuleDescription>
            {
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "Address",
                    OnFailure = "Continue",
                    ValidatorName = "AddressValidator",
					ValidationMessage = null
                }
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
