using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System.Collections.Generic;
using ValiDoc.Output;
using ValiDoc.Utility;

namespace ValiDoc.Core
{
	public static class RuleDescriptionBuilder
    {
	    public static IEnumerable<RuleDescription> BuildRuleDescription(IPropertyValidator validationRules, string propertyName, CascadeMode cascadeMode, bool documentNested, PropertyRule rule)
	    {
		    string validatorName;
		    Severity? validationFailureSeverity;
		    string validationMessage = null;

		    // Check whether the rule in question is another validator (ChildValidator)
		    if (validationRules is ChildValidatorAdaptor childValidator)
		    {
			    validatorName = childValidator.ValidatorType.Name;
			    validationFailureSeverity = childValidator.Severity;

			    if (documentNested)
			    {
				    //Recursively document the validator rules
				    foreach (var ruleDescription in RecursiveDocument.GetNestedRules(propertyName, rule, childValidator))
					    yield return ruleDescription;
			    }
		    }
		    else
		    {
			    validatorName = validationRules.GetType().Name;
			    validationFailureSeverity = validationRules.Severity;
			    validationMessage = ValidationMessageBuilder.GetValidationMessage(validationRules, rule, propertyName);
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
