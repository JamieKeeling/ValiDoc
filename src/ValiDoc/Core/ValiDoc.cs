using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;

namespace ValiDoc
{
    public static class ValiDoc
    {
        public static IEnumerable<string> GetRules<T>(this AbstractValidator<T> validator)
        {
            if(validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            var descriptor = validator.CreateDescriptor();

            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            var memberValidators = descriptor.GetMembersWithValidators();

            foreach(var member in memberValidators)
            {
                var rules = descriptor.GetRulesForMember(member.Key);
                
                foreach(PropertyRule rule in rules)
                {
                    var propertyName = rule.GetDisplayName();

                    //TODO: Identify supplied parameters for bounds based on the validator (example Maximum of 20, etc..)
                    foreach(var validationRules in rule.Validators)
                    {
                        if (validationRules is ChildValidatorAdaptor childValidator)
                        {
                            yield return $"Field: {propertyName} | Validation: {childValidator.ValidatorType.FullName} | Severity: {childValidator.Severity}";
                            yield break;
                        }

                        yield return $"Field: {propertyName} | Validation: {validationRules.ErrorMessageSource.ResourceName} | Severity: {validationRules.Severity}";
                    }
                }
            }
        }
    }
}
