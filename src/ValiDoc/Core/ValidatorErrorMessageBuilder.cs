using System;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using ValiDoc.Extensions;
using ValiDoc.Utility;

namespace ValiDoc.Core
{
    public class ValidatorErrorMessageBuilder : IValidatorErrorMessageBuilder
    {
        private readonly IFluentValidationHelper _fluentValidationHelper;

        public ValidatorErrorMessageBuilder(IFluentValidationHelper fluentValidationHelper)
        {
            _fluentValidationHelper = fluentValidationHelper;
        }

	    public string GetErrorMessage(IPropertyValidator validator, PropertyRule rule, string propertyName)
	    {
	        if (validator == null)
	            throw new ArgumentNullException(nameof(validator));

	        if (rule == null)
	            throw new ArgumentNullException(nameof(rule));

	        if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

		    const string messagePreparationMethodIdentifier = "PrepareMessageFormatterForValidationError";
		    const string createValidationErrorMethodIdentifier = "CreateValidationError";

		    var validatorMethods = validator.GetType().ExtractMethodInfo(new[] { messagePreparationMethodIdentifier, createValidationErrorMethodIdentifier });

	        if (validatorMethods == null)
	            return null;

            //ValidatorContext is based on the type passed into the validator that is validated against, such as a 'Person' POCO
            var validatorContext = _fluentValidationHelper.BuildPropertyValidatorContext(rule, propertyName);

            //Force the validator to populate field parameter names that are used as part of the error response
            validatorMethods[messagePreparationMethodIdentifier].Invoke(validator, new object[] { validatorContext });

            //With the parameter names completed, build the error using the Validator instance
		    var validationFailure = validatorMethods[createValidationErrorMethodIdentifier].Invoke(validator, new object[] { validatorContext }) as ValidationFailure;

		    return validationFailure?.ErrorMessage;
	    }
	}
}
