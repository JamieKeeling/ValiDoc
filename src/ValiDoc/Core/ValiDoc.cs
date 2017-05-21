using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;

namespace ValiDoc.Core
{
    public class ValiDoc
    {
        public IEnumerable<string> GetRules<T>(AbstractValidator<T> validator)
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

            var validators = descriptor.GetMembersWithValidators();

            foreach(var propertyValidator in validators)
            {
                var rules = descriptor.GetRulesForMember(propertyValidator.Key);
                
                foreach(PropertyRule rule in rules)
                {
                    var propertyName = rule.GetDisplayName();

                    //TODO: Identify supplied parameters for bounds based on the validator (example Maximum of 20, etc..)
                    foreach(PropertyValidator validationRules in rule.Validators)
                    {
                        yield return $"Field: {propertyName} | Validation: {validationRules.ErrorMessageSource.ResourceName} | Severity: {validationRules.Severity}";
                    }
                }
            }
        }
    }
}
