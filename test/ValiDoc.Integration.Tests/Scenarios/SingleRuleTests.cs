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
    public class SingleRuleTests
    {
        [Fact]
        public void ValiDoc_WithSingleRuleValidator_OutputSingleRule()
        {
            var validator = new SingleRuleValidator();

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
                }
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }
    }
}
