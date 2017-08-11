using System;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Moq;
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

            var expectedRuleDescription = new RuleDescription
            {
                FailureSeverity = "Error",
                MemberName = "First Name",
                OnFailure = "Continue",
                ValidationMessage = validationErrorMessage,
                ValidatorName = "NotEmptyValidator"
            };

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(mockValidatorErrorMessageBuilder.Object);

            var actualRuleDescription = ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators.Single(), propertyName, cascadeMode, propertyRule).ToList();

            actualRuleDescription.Count.Should().Be(1);

            actualRuleDescription.First().ShouldBeEquivalentTo(expectedRuleDescription);
        }

        [Fact]
        public void RuleDescriptionBuilder_WithSingleChildRuleValidator_ReturnsChildRuleValidatorRuleDescription()
        {
            const CascadeMode cascadeMode = CascadeMode.Continue;

            var validator = new SingleChildValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember(validator.CreateDescriptor().GetMembersWithValidators().First().Key).First() as PropertyRule;
            var propertyName = propertyRule.GetDisplayName();

            var expectedRuleDescription = new RuleDescription
            {
                FailureSeverity = "Error",
                MemberName = "Address",
                OnFailure = "Continue",
                ValidationMessage = "N/A - Refer to specific AddressValidator documentation",
                ValidatorName = "AddressValidator"
            };

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(Mock.Of<IValidatorErrorMessageBuilder>());

            var actualRuleDescription = ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators.Single(), propertyName, cascadeMode, propertyRule).ToList();

            actualRuleDescription.Count.Should().Be(1);

            actualRuleDescription.First().ShouldBeEquivalentTo(expectedRuleDescription);
        }

        [Fact]
        public void RuleDescriptionBuilder_WithNullValidationRule_ThrowsArgumentNullException()
        {
            const CascadeMode cascadeMode = CascadeMode.Continue;

            var validator = new SingleChildValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember(validator.CreateDescriptor().GetMembersWithValidators().First().Key).First() as PropertyRule;
            var propertyName = propertyRule.GetDisplayName();

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(Mock.Of<IValidatorErrorMessageBuilder>());

            var expectedExcepton = Record.Exception(() => ruleDescriptionBuilder.BuildRuleDescription(null, propertyName, cascadeMode, propertyRule).ToList());

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("validationRules");
        }

        [Fact]
        public void RuleDescriptionBuilder_WithNullPropertyName_ThrowsArgumentNullException()
        {
            const CascadeMode cascadeMode = CascadeMode.Continue;

            var validator = new SingleChildValidator();
            var propertyRule = validator.CreateDescriptor().GetRulesForMember(validator.CreateDescriptor().GetMembersWithValidators().First().Key).First() as PropertyRule;

            var ruleDescriptionBuilder = new RuleDescriptionBuilder(Mock.Of<IValidatorErrorMessageBuilder>());

            var expectedExcepton = Record.Exception(() => ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators.Single(), null, cascadeMode, propertyRule).ToList());

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

            var expectedExcepton = Record.Exception(() => ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators.Single(), string.Empty, cascadeMode, propertyRule).ToList());

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

            var expectedExcepton = Record.Exception(() => ruleDescriptionBuilder.BuildRuleDescription(propertyRule.Validators.Single(), " ", cascadeMode, propertyRule).ToList());

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("propertyName");
        }
    }
}
