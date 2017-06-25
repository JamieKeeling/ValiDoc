using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System;

namespace ValiDoc.Utility
{
	public static class FluentValidationHelpers
    {
	    public static PropertyValidatorContext BuildPropertyValidatorContext(PropertyRule rule, string propertyName)
	    {
		    return new PropertyValidatorContext(new ValidationContext(Activator.CreateInstance(rule.Member.DeclaringType)), rule, propertyName);
	    }
	}
}
