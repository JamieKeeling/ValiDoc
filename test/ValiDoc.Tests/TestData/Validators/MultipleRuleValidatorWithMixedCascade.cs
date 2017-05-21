using FluentValidation;
using ValiDoc.Tests.TestData.POCOs;

namespace ValiDoc.Tests.TestData.Validators
{
    public class MultipleRuleValidatorWithMixedCascade : AbstractValidator<Person>
	{
		public MultipleRuleValidatorWithMixedCascade()
		{
            CascadeMode = CascadeMode.Continue;

			RuleFor(p => p.FirstName).NotEmpty();
			RuleFor(p => p.LastName).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().MaximumLength(20);
		}
	}
}