using FluentValidation;
using ValiDoc.Tests.TestData.POCOs;

namespace ValiDoc.Tests.TestData.Validators
{
    public class MultipleRuleValidatorWithMixedCascade : AbstractValidator<Person>
	{
		public MultipleRuleValidatorWithMixedCascade()
		{
            CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(p => p.FirstName).NotEmpty();
			RuleFor(p => p.LastName).Cascade(CascadeMode.Continue).NotEmpty().MaximumLength(20);
		}
	}
}