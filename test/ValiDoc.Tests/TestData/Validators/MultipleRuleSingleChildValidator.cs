using FluentValidation;
using ValiDoc.Tests.TestData.POCOs;

namespace ValiDoc.Tests.TestData.Validators
{
    public class MultipleRuleSingleChildValidator : AbstractValidator<Person>
	{
		public MultipleRuleSingleChildValidator()
		{
			RuleFor(p => p.FirstName).NotEmpty();
			RuleFor(p => p.LastName).NotEmpty().MaximumLength(20);
            RuleFor(p => p.Address).SetValidator(new AddressValidator());
		}
	}
}