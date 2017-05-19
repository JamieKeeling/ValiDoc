using FluentAssertions;
using Xunit;
using ValiDoc.Tests.TestData;

namespace ValiDoc.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void HasBaseToWorkFrom()
        {
            var person = new Person("Jamie", "Keeling");

            var personValidator = new PersonValidator();

            var validationOutput = personValidator.Validate(person);

            validationOutput.Errors.Should().BeEmpty();
        }
    }
}