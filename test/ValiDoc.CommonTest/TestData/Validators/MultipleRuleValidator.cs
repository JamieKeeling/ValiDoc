using FluentValidation;
using ValiDoc.CommonTest.TestData.POCOs;

namespace ValiDoc.CommonTest.TestData.Validators
{
    public class MultipleRuleValidator : AbstractValidator<Person>
	{
		public MultipleRuleValidator()
		{
			RuleFor(p => p.FirstName).NotNull();
			RuleFor(p => p.LastName).NotEmpty().MinimumLength(3).MaximumLength(20);
		}
	}
}