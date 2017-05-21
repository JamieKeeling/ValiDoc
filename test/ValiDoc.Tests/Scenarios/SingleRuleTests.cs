using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using ValiDoc.Output;
using ValiDoc.Tests.TestData.Validators;
using Xunit;

namespace ValiDoc.Tests.Scenarios
{
    public class SingleRuleTests
    {
        [Fact]
        public void ValiDoc_WithSingleRuleValidator_OutputSingleRule()
        {
            var validator = new SingleRuleValidator();

            var validationRules = validator.GetRules().ToList();

            var expectedOutput = new List<RuleDescription>
            {
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    MemberName = "First Name",
                    OnFailure = "Continue",
                    ValidatorName = "NotEmptyValidator"
                }
            };

            validationRules.ShouldBeEquivalentTo(expectedOutput);
        }
    }
}
