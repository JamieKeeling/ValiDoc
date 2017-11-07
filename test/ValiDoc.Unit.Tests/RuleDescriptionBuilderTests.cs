using FluentAssertions;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using ValiDoc.CommonTest.TestData.Validators;
using ValiDoc.Core;
using ValiDoc.Output;
using Xunit;

namespace ValiDoc.Unit.Tests
{
    public class RuleDescriptionBuilderTests
    {
        [Fact]
        public void RuleDescriptionBuilder_WithSingleRuleValidator_ReturnsSingleRuleDescription()
        {
            const string validationErrorMessage = "Mock message";
            const CascadeMode cascadeMode = CascadeMode.Continue;

            var mockValidatorErrorMessageBuilder = new Mock<IValidatorErrorMessageBuilder>();
            mockValidatorErrorMessageBuilder.Setup(m => m.GetErrorMessage(It.IsAny<IPropertyValidator>(), It.IsAny<PropertyRule>(), It.IsAny<string>()))
                                                .Returns(validationErrorMessage);

            var validator = new SingleRuleValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember("FirstName").First() as PropertyRule;
            var propertyName = propertyRule.GetDisplayName();

            var expectedRuleDescription = new RuleDescriptor
            {
                MemberName = "First Name",
                Rules = new List<RuleDescription>
                {
                    new RuleDescription
                    {
                        FailureSeverity = "Error",
                        OnFailure = "Continue",
                        ValidationMessage = validationErrorMessage,
                        ValidatorName = "NotEmptyValidator"
                    }
                }
            };

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(mockValidatorErrorMessageBuilder.Object);

            var actualRuleDescription = ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators, propertyName, cascadeMode, propertyRule);

            actualRuleDescription.Rules.Should().HaveCount(1);
            actualRuleDescription.ShouldBeEquivalentTo(expectedRuleDescription);
        }

        [Fact]
        public void RuleDescriptionBuilder_WithSingleChildRuleValidator_ReturnsChildRuleValidatorRuleDescription()
        {
            const CascadeMode cascadeMode = CascadeMode.Continue;

            var validator = new SingleChildValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember(validator.CreateDescriptor().GetMembersWithValidators().First().Key).First() as PropertyRule;
            var propertyName = propertyRule.GetDisplayName();

            var expectedRuleDescription = new RuleDescriptor
            {
                MemberName = "Address",
                Rules = new List<RuleDescription>
                {
                    new RuleDescription
                    {
                        FailureSeverity = "Error",
                        OnFailure = "Continue",
                        ValidationMessage = "N/A - Refer to specific AddressValidator documentation",
                        ValidatorName = "AddressValidator"
                    }
                }
            };

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(Mock.Of<IValidatorErrorMessageBuilder>());

            var actualRuleDescription = ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators, propertyName, cascadeMode, propertyRule);

            actualRuleDescription.Rules.Should().HaveCount(1);
            actualRuleDescription.ShouldBeEquivalentTo(expectedRuleDescription);
        }

        [Fact]
        public void RuleDescriptionBuilder_WithMultipleRuleValidator_ReturnsStructuredRuleDescription()
        {
            const string validationErrorMessage = "Mock message";
            var validator = new MultipleRuleValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember(validator.CreateDescriptor().GetMembersWithValidators().Last().Key).First() as PropertyRule;
            var propertyName = propertyRule.GetDisplayName();

            var mockValidatorErrorMessageBuilder = new Mock<IValidatorErrorMessageBuilder>();
            mockValidatorErrorMessageBuilder.Setup(m => m.GetErrorMessage(It.IsAny<IPropertyValidator>(), It.IsAny<PropertyRule>(), It.IsAny<string>()))
                .Returns(validationErrorMessage);

            var expectedOutput = new RuleDescriptor
            {
                MemberName = "Last Name",
                Rules = new List<RuleDescription>
                {
                    new RuleDescription
                    {
                        FailureSeverity = "Error",
                        OnFailure = "Continue",
                        ValidationMessage = validationErrorMessage,
                        ValidatorName = "NotEmptyValidator"
                    },
                    new RuleDescription
                    {
                        FailureSeverity = "Error",
                        OnFailure = "Continue",
                        ValidationMessage = validationErrorMessage,
                        ValidatorName = "MinimumLengthValidator"
                    },
                    new RuleDescription
                    {
                        FailureSeverity = "Error",
                        OnFailure = "Continue",
                        ValidationMessage = validationErrorMessage,
                        ValidatorName = "MaximumLengthValidator"
                    }
                }
            };


            var ruleDescriptionBuilder = new RuleDescriptionBuilder(mockValidatorErrorMessageBuilder.Object);

            var actualRuleDescription = ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators, propertyName, CascadeMode.Continue, propertyRule);

            actualRuleDescription.Rules.Should().HaveCount(3);
            actualRuleDescription.ShouldBeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }

        [Fact]
        public void RuleDescriptionBuilder_WithNullValidationRule_ThrowsArgumentNullException()
        {
            const CascadeMode cascadeMode = CascadeMode.Continue;

            var validator = new SingleChildValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember(validator.CreateDescriptor().GetMembersWithValidators().First().Key).First() as PropertyRule;
            var propertyName = propertyRule.GetDisplayName();

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(Mock.Of<IValidatorErrorMessageBuilder>());

            var expectedExcepton = Record.Exception(() => ruleDescriptionBuilder.BuildRuleDescription(null, propertyName, cascadeMode, propertyRule));

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("validationRules");
        }

        [Fact]
        public void RuleDescriptionBuilder_WithEmptyValidationRule_ThrowsInvalidOperationException()
        {
            const CascadeMode cascadeMode = CascadeMode.Continue;

            var validator = new SingleChildValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember(validator.CreateDescriptor().GetMembersWithValidators().First().Key).First() as PropertyRule;
            var propertyName = propertyRule.GetDisplayName();

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(Mock.Of<IValidatorErrorMessageBuilder>());

            var expectedExcepton = Record.Exception(() => ruleDescriptionBuilder.BuildRuleDescription(Enumerable.Empty<IPropertyValidator>(), propertyName, cascadeMode, propertyRule));

            expectedExcepton.Should().BeOfType<InvalidOperationException>();
            expectedExcepton.Message.Contains("No validation rules to document");
        }

        [Fact]
        public void RuleDescriptionBuilder_WithNullPropertyName_ThrowsArgumentNullException()
        {
            const CascadeMode cascadeMode = CascadeMode.Continue;

            var validator = new SingleChildValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember(validator.CreateDescriptor().GetMembersWithValidators().First().Key).First() as PropertyRule;

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(Mock.Of<IValidatorErrorMessageBuilder>());

            var expectedExcepton = Record.Exception(() => ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators, null, cascadeMode, propertyRule));

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("propertyName");
        }

        [Fact]
        public void RuleDescriptionBuilder_WithEmptyPropertyName_ThrowsArgumentNullException()
        {
            const CascadeMode cascadeMode = CascadeMode.Continue;

            var validator = new SingleChildValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember(validator.CreateDescriptor().GetMembersWithValidators().First().Key).First() as PropertyRule;

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(Mock.Of<IValidatorErrorMessageBuilder>());

            var expectedExcepton = Record.Exception(() => ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators, string.Empty, cascadeMode, propertyRule));

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("propertyName");
        }

        [Fact]
        public void RuleDescriptionBuilder_WithWhitespacePropertyName_ThrowsArgumentNullException()
        {
            const CascadeMode cascadeMode = CascadeMode.Continue;

            var validator = new SingleChildValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember(validator.CreateDescriptor().GetMembersWithValidators().First().Key).First() as PropertyRule;

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(Mock.Of<IValidatorErrorMessageBuilder>());

            var expectedExcepton = Record.Exception(() => ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators, " ", cascadeMode, propertyRule));

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("propertyName");
        }
    }
}
