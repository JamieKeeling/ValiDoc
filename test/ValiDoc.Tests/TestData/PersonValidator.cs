using FluentValidation;

namespace ValiDoc.Tests.TestData
{
    public class PersonValidator : AbstractValidator<Person>
	{
		public PersonValidator()
		{
			RuleFor(p => p.FirstName).NotEmpty();
			RuleFor(p => p.LastName).NotEmpty().MaximumLength(20);
		}
	}
}