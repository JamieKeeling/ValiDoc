using FluentValidation;
using ValiDoc.Tests.TestData.POCOs;

namespace ValiDoc.Tests.TestData.Validators
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