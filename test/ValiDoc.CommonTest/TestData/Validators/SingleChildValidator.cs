using FluentValidation;
using ValiDoc.CommonTest.TestData.POCOs;

namespace ValiDoc.CommonTest.TestData.Validators
{
    public class SingleChildValidator : AbstractValidator<Person>
	{
		public SingleChildValidator()
		{
            RuleFor(p => p.Address).SetValidator(new AddressValidator());
		}
	}
}