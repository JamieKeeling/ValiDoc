using FluentValidation;
using ValiDoc.Tests.TestData.POCOs;

namespace ValiDoc.Tests.TestData.Validators
{
    public class SingleRuleValidator : AbstractValidator<Person>
	{
		public SingleRuleValidator()
		{
			RuleFor(p => p.FirstName).NotEmpty();
		}
	}
}