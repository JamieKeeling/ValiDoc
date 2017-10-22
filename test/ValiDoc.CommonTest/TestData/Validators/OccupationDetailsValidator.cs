using FluentValidation;
using ValiDoc.CommonTest.TestData.POCOs;

namespace ValiDoc.CommonTest.TestData.Validators
{
    public class OccupationDetailsValidator : AbstractValidator<OccupationDetails>
    {
        public OccupationDetailsValidator()
        {
            RuleFor(p => p.EmploymentStatus).NotEqual(EmploymentStatus.NotSet);
            RuleFor(p => p.EmploymentStatus).IsInEnum();
            RuleFor(p => p.JobTitle).NotEmpty();
        }
    }
}
