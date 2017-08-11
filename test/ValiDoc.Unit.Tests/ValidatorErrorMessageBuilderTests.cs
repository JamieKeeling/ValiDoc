using System;
using System.Linq;
using FluentValidation.Internal;
using Xunit;
using ValiDoc.CommonTest.TestData.Validators;
using ValiDoc.Core;
using ValiDoc.Utility;
using FluentAssertions;

namespace ValiDoc.Unit.Tests
{
    public class ValidatorErrorMessageBuilderTests
    {
        [Fact]
        public void ValidationMessageBuilder_WithValidInput_BuildsValidationMessages()
        {
            const string expectedErrorMessage = "'First Name' should not be empty.";
            var validator = new SingleRuleValidator();
            var validationErrorMessageBuilder = new ValidatorErrorMessageBuilder(new FluentValidationHelpers());

            var propertyRule = validator.CreateDescriptor().GetRulesForMember("FirstName").First() as PropertyRule;
            var propertyName = propertyRule.GetDisplayName();
            
            var actualErrorMessage = validationErrorMessageBuilder.GetErrorMessage(propertyRule.Validators.Single(), propertyRule, propertyName);

            actualErrorMessage.ShouldBeEquivalentTo(expectedErrorMessage);
        }

        [Fact]
        public void ValidationMessageBuilder_WithMissingValidator_ThrowsArgumentNullException()
        {
            var validator = new SingleRuleValidator();
            var validationErrorMessageBuilder = new ValidatorErrorMessageBuilder(new FluentValidationHelpers());

            var propertyRule = validator.CreateDescriptor().GetRulesForMember("FirstName").First() as PropertyRule;
            var propertyName = propertyRule.GetDisplayName();

            var expectedExcepton = Record.Exception(() => validationErrorMessageBuilder.GetErrorMessage(null, propertyRule, propertyName));

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("validator");
        }

        [Fact]
        public void ValidationMessageBuilder_WithMissingProperyRule_ThrowsArgumentNullException()
        {
            var validator = new SingleRuleValidator();
            var validationErrorMessageBuilder = new ValidatorErrorMessageBuilder(new FluentValidationHelpers());

            var propertyRule = validator.CreateDescriptor().GetRulesForMember("FirstName").First() as PropertyRule;
            var propertyName = propertyRule.GetDisplayName();

            var expectedExcepton = Record.Exception(() => validationErrorMessageBuilder.GetErrorMessage(propertyRule.Validators.Single(), null, propertyName));

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("rule");
        }

        [Fact]
        public void ValidationMessageBuilder_WithEmptyPropertyName_ThrowsArgumentNullException()
        {
            var validator = new SingleRuleValidator();
            var validationErrorMessageBuilder = new ValidatorErrorMessageBuilder(new FluentValidationHelpers());

            var propertyRule = validator.CreateDescriptor().GetRulesForMember("FirstName").First() as PropertyRule;

            var expectedExcepton = Record.Exception(() => validationErrorMessageBuilder.GetErrorMessage(propertyRule.Validators.Single(), propertyRule, string.Empty));

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("propertyName");
        }

        [Fact]
        public void ValidationMessageBuilder_WithNullPropertyName_ThrowsArgumentNullException()
        {
            var validator = new SingleRuleValidator();
            var validationErrorMessageBuilder = new ValidatorErrorMessageBuilder(new FluentValidationHelpers());

            var propertyRule = validator.CreateDescriptor().GetRulesForMember("FirstName").First() as PropertyRule;

            var expectedExcepton = Record.Exception(() => validationErrorMessageBuilder.GetErrorMessage(propertyRule.Validators.Single(), propertyRule, null));

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("propertyName");
        }

        [Fact]
        public void ValidationMessageBuilder_WithWhitespacePropertyName_ThrowsArgumentNullException()
        {
            var validator = new SingleRuleValidator();
            var validationErrorMessageBuilder = new ValidatorErrorMessageBuilder(new FluentValidationHelpers());

            var propertyRule = validator.CreateDescriptor().GetRulesForMember("FirstName").First() as PropertyRule;

            var expectedExcepton = Record.Exception(() => validationErrorMessageBuilder.GetErrorMessage(propertyRule.Validators.Single(), propertyRule, " "));

            expectedExcepton.Should().BeOfType<ArgumentNullException>();
            expectedExcepton.Message.Contains("propertyName");
        }
    }
}
