using Xunit;
using FluentAssertions;

namespace ValiDoc.Tests
{
    public class ValiDocBehaviour
	{
		[Fact]
		public void ValiDoc_WithValidator_OutputsBaseRules()
		{
            var valiDoc = new Core.ValiDoc();

            //var validationRules = valiDoc.Rules(new PersonValidator());

            //validationRules.Should().NotBeEmpty();

            var x = new PersonValidator().CreateDescriptor();

            var y = x.GetMembersWithValidators();
		}
	}
}
