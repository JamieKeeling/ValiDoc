using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using ValiDoc.Extensions;
using ValiDoc.Utility;

namespace ValiDoc.Core
{
	public static class ValidationMessageBuilder
    {
	    public static string GetValidationMessage(IPropertyValidator validator, PropertyRule rule, string propertyName)
	    {
		    const string messagePreparationMethodIdentifier = "PrepareMessageFormatterForValidationError";
		    const string createValidationErrorMethodIdentifier = "CreateValidationError";

		    var validatorMethods = validator.GetType().ExtractMethodInfo(new[] { messagePreparationMethodIdentifier, createValidationErrorMethodIdentifier });

		    if (validatorMethods == null)
			    return null;

		    //Context should be the instance passed into "Validate", however as Document does not expect one we should just create an instance manually
		    //TODO: Is there any way we can ensure we ALWAYS raise the error cases for a validator with the default type
		    // e.g. Default type value accidentaly 'passes' rules that would normally fail..?
		    var validatorContext = FluentValidationHelpers.BuildPropertyValidatorContext(rule, propertyName);

		    validatorMethods[messagePreparationMethodIdentifier].Invoke(validator, new object[] { validatorContext });
		    var validationFailure = validatorMethods[createValidationErrorMethodIdentifier].Invoke(validator, new object[] { validatorContext }) as ValidationFailure;

		    return validationFailure?.ErrorMessage;
	    }
	}
}
