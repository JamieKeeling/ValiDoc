using FluentValidation;
using ValiDoc.Tests.TestData.POCOs;

namespace ValiDoc.Tests.TestData.Validators
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
