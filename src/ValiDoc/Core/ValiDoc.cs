using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using ValiDoc.Output;

namespace ValiDoc
{
    //TODO: This class is huge - should investigate splitting out dependencies
    // and relying on a DI container based on the consumer using the library.
    public static class ValiDoc
    {
        public static IEnumerable<RuleDescription> GetRules<T>(this AbstractValidator<T> validator)
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
                
                foreach(var validationRule in rules)
                {
                    var rule = (PropertyRule) validationRule;
                    var propertyName = rule.GetDisplayName();

                    //TODO: Identify supplied parameters for bounds based on the validator (example Maximum of 20, etc..)
                    foreach(var validationRules in rule.Validators)
                    {
                        yield return BuildRuleDescription(validationRules, propertyName);
                    }
                }
            }
        }

        private static RuleDescription BuildRuleDescription(IPropertyValidator validationRules, string propertyName)
        {
            string validatorName;
            Severity? validationFailureSeverity;

            if (validationRules is ChildValidatorAdaptor childValidator)
            {
                validatorName = childValidator.ValidatorType.Name;
                validationFailureSeverity = childValidator.Severity;
            }
            else
            {
                //Rules are not from a subsequent validator
                validatorName = validationRules.ErrorMessageSource.ResourceName;
                validationFailureSeverity = validationRules.Severity;
            }

            return new RuleDescription
            {
                MemberName = propertyName,
                ValidatorName = validatorName,
                FailureSeverity = validationFailureSeverity.ToString()
            };
        }
    }
}
