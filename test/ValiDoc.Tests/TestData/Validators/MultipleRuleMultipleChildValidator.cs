using FluentValidation;
using ValiDoc.Tests.TestData.POCOs;

namespace ValiDoc.Tests.TestData.Validators
{
    public class MultipleRuleMultipleChildValidator : AbstractValidator<Person>
    {
        public MultipleRuleMultipleChildValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty().MaximumLength(20);
            RuleFor(p => p.Address).SetValidator(new AddressValidator());
            RuleFor(p => p.OccupationDetails).SetValidator(new OccupationDetailsValidator());
        }
    }
}