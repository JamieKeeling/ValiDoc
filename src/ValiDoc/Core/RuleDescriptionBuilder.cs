using System;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System.Collections.Generic;
using ValiDoc.Output;

namespace ValiDoc.Core
{
    public class RuleDescriptionBuilder : IRuleDescriptor
    {
        private readonly IValidatorErrorMessageBuilder _validatorErrorMessageBuilder;

        public RuleDescriptionBuilder(IValidatorErrorMessageBuilder validatorErrorMessageBuilder)
        {
            _validatorErrorMessageBuilder = validatorErrorMessageBuilder;
        }

	    public IEnumerable<RuleDescription> BuildRuleDescription(IPropertyValidator validationRules, string propertyName, CascadeMode cascadeMode, PropertyRule rule)
	    {
	        if (validationRules == null)
                throw new ArgumentNullException(nameof(validationRules));

            if(string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

		    string validatorName;
		    Severity? validationFailureSeverity;
		    string validationMessage = null;
            
		    if (validationRules is ChildValidatorAdaptor childValidator)
		    {
			    validatorName = childValidator.ValidatorType.Name;
			    validationFailureSeverity = childValidator.Severity;
		        validationMessage = $"N/A - Refer to specific {validatorName} documentation";
		    }
		    else
		    {
			    validatorName = validationRules.GetType().Name;
			    validationFailureSeverity = validationRules.Severity;
			    validationMessage = _validatorErrorMessageBuilder.GetErrorMessage(validationRules, rule, propertyName);
		    }

		    yield return new RuleDescription
		    {
			    MemberName = propertyName,
			    ValidatorName = validatorName,
			    FailureSeverity = validationFailureSeverity.ToString(),
			    OnFailure = cascadeMode.ToString(),
			    ValidationMessage = validationMessage
		    };
	    }
	}
}
