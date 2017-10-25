using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using ValiDoc.Output;

namespace ValiDoc.Core
{
    public class RuleDescriptionBuilder : IRuleBuilder
    {
        private readonly IValidatorErrorMessageBuilder _validatorErrorMessageBuilder;

        public RuleDescriptionBuilder(IValidatorErrorMessageBuilder validatorErrorMessageBuilder)
        {
            _validatorErrorMessageBuilder = validatorErrorMessageBuilder;
        }

	    public RuleDescriptor BuildRuleDescription(IEnumerable<IPropertyValidator> validationRules, string propertyName, CascadeMode cascadeMode, PropertyRule rule)
	    {
	        if (validationRules == null)
                throw new ArgumentNullException(nameof(validationRules));

	        var propertyValidators = validationRules as IList<IPropertyValidator> ?? validationRules.ToList();

	        if (!propertyValidators.Any())
	            throw new InvalidOperationException("No validation rules to document");

            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

	        var ruleDescriptor = new RuleDescriptor
	        {
	            MemberName = propertyName
	        };

	        var ruleDescriptions = new List<RuleDescription>();

	        foreach (var propertyValidator in propertyValidators)
	        {
	            string validatorName;
	            Severity? validationFailureSeverity;
	            string validationMessage;

	            if (propertyValidator is ChildValidatorAdaptor childValidator)
	            {
	                validatorName = childValidator.ValidatorType.Name;
	                validationFailureSeverity = childValidator.Severity;
	                validationMessage = $"N/A - Refer to specific {validatorName} documentation";
	            }
	            else
	            {
	                validatorName = propertyValidator.GetType().Name;
	                validationFailureSeverity = propertyValidator.Severity;
	                validationMessage = _validatorErrorMessageBuilder.GetErrorMessage(propertyValidator, rule, propertyName);
	            }

	            ruleDescriptions.Add(new RuleDescription
	            {
	                ValidatorName = validatorName,
	                FailureSeverity = validationFailureSeverity.ToString(),
	                OnFailure = cascadeMode.ToString(),
	                ValidationMessage = validationMessage
	            });
	        }

	        ruleDescriptor.Rules = ruleDescriptions;

	        return ruleDescriptor;
	    }
	}
}
