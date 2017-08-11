using FluentValidation;
using ValiDoc.CommonTest.TestData.POCOs;

namespace ValiDoc.CommonTest.TestData.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.HouseNumber).NotEmpty();
            RuleFor(address => address.StreetName).NotEmpty();
            RuleFor(address => address.PostCode).NotEmpty();
        }
    }
}
