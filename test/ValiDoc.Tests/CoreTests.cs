using FluentAssertions;
using ValiDoc.Tests.TestData;
using Xunit;

namespace ValiDoc.Tests
{
    public class ValiDocBehaviour
	{
		[Fact]
		public void ValiDoc_WithValidator_OutputsBaseRules()
		{
            var valiDoc = new Core.ValiDoc();

            var validationRules = valiDoc.GetRules(new PersonValidator());

            validationRules.Should().NotBeEmpty();
		}
	}
}
