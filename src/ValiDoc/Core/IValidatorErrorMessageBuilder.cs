using FluentValidation.Internal;
using FluentValidation.Validators;

namespace ValiDoc.Core
{
    public interface IValidatorErrorMessageBuilder
    {
        string GetErrorMessage(IPropertyValidator validator, PropertyRule rule, string propertyName);
    }
}
