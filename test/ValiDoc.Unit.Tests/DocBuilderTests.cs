using FluentAssertions;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using ValiDoc.CommonTest.TestData.POCOs;
using ValiDoc.CommonTest.TestData.Validators;
using ValiDoc.Core;
using ValiDoc.Output;
using Xunit;

namespace ValiDoc.Unit.Tests
{
    public class DocBuilderTests
    {
        [Fact]
        public void DocBuilder_WithValidator_ReturnsRuleDescription()
        {
            var singleRuleValidator = new SingleRuleValidator();

            var expectedRuleDescription = new RuleDescriptor
            {
                MemberName = "First Name",
                Rules = new List<RuleDescription>
                {
                new RuleDescription
                {
                    FailureSeverity = "Error",
                    OnFailure = "Continue",
                    ValidationMessage = "Mock message",
                    ValidatorName = "NotEmptyValidator"
                }
                }
            };

            var mockRuleDescriptor = new Mock<IRuleBuilder>();
            mockRuleDescriptor.Setup(x => x.BuildRuleDescription(It.IsAny<IEnumerable<IPropertyValidator>>(), It.IsAny<string>(),
                    It.IsAny<CascadeMode>(), It.IsAny<PropertyRule>())).Returns(expectedRuleDescription);

            var docBuilder = new DocBuilder(mockRuleDescriptor.Object);

            var actualResult = docBuilder.Document(singleRuleValidator).ToList();

            actualResult.Should().HaveCount(1);
            actualResult.Should().BeEquivalentTo(expectedRuleDescription);
        }

        [Fact]
        public void DocBuilder_WithNullValidator_ThrowsArgumentNullException()
        {
            var docBuilder = new DocBuilder(Mock.Of<IRuleBuilder>());

            var thrownException = Record.Exception(() => docBuilder.Document<AbstractValidator<Address>>(null).ToList()); //.ToList() to materialise exception

            thrownException.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("validator");
        }

        [Fact]
        public void DocBuilder_WithNoRuleValidator_ReturnEmptyOutput()
        {
            var docBuilder = new DocBuilder(Mock.Of<IRuleBuilder>());

            var actualOutput = docBuilder.Document(new NoRulesValidator());

            actualOutput.Should().BeEquivalentTo(Enumerable.Empty<RuleDescription>());
        }
    }
}
