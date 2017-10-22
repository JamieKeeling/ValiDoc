using FluentValidation;
using ValiDoc.CommonTest.TestData.POCOs;

namespace ValiDoc.CommonTest.TestData.Validators
{
    public class MultipleRuleValidatorWithGlobalCascade : AbstractValidator<Person>
	{
		public MultipleRuleValidatorWithGlobalCascade()
		{
            CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(p => p.FirstName).NotEmpty();
			RuleFor(p => p.LastName).NotEmpty().MaximumLength(20);
		}
	}
}