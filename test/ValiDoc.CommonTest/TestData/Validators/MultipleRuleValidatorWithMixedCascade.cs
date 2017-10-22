using FluentValidation;
using ValiDoc.CommonTest.TestData.POCOs;

namespace ValiDoc.CommonTest.TestData.Validators
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