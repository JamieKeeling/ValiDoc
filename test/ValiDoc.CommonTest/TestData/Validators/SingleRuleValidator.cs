using FluentValidation;
using ValiDoc.CommonTest.TestData.POCOs;

namespace ValiDoc.CommonTest.TestData.Validators
{
    public class SingleRuleValidator : AbstractValidator<Person>
	{
		public SingleRuleValidator()
		{
			RuleFor(p => p.FirstName).NotEmpty();
		}
	}
}