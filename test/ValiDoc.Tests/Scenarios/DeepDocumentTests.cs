using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var validationRules = validator.GetRules().ToList();

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
                    MemberName = "Address",
                    OnFailure = "Continue",
                    ValidatorName = "AddressValidator"
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
                }
            };
        }
    }
}
