using FluentValidation.Internal;
using FluentValidation.Validators;

namespace ValiDoc.Utility
{
    public interface IFluentValidationHelper
    {
        PropertyValidatorContext BuildPropertyValidatorContext(PropertyRule rule, string propertyName);
    }
}
