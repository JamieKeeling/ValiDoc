using FluentValidation;
using ValiDoc.Tests.TestData.POCOs;

namespace ValiDoc.Tests.TestData.Validators
{
    public class SingleChildValidator : AbstractValidator<Person>
	{
		public SingleChildValidator()
		{
            RuleFor(p => p.Address).SetValidator(new AddressValidator());
		}
	}
}