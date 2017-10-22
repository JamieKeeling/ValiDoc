using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System;

namespace ValiDoc.Utility
{
    public class FluentValidationHelpers : IFluentValidationHelper
    {
	    public PropertyValidatorContext BuildPropertyValidatorContext(PropertyRule rule, string propertyName)
	    {
		    return new PropertyValidatorContext(new ValidationContext(Activator.CreateInstance(rule.Member.DeclaringType)), rule, propertyName);
	    }
	}
}
