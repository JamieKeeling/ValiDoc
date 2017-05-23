using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Reflection;
using ValiDoc.Output;

namespace ValiDoc
{
    //TODO: This class is huge - should investigate splitting out dependencies
    // and relying on a DI container based on the consumer using the library.
    public static class ValiDoc
    {
        public static IEnumerable<RuleDescription> GetRules<T>(this IValidator<T> validator, bool documentNested = false)
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
                        yield return BuildRuleDescription<T>(validationRules, propertyName, rule.CascadeMode, documentNested, rule);
                    }
                }
            }
        }

        private static RuleDescription BuildRuleDescription<T>(IPropertyValidator validationRules, string propertyName, CascadeMode cascadeMode, bool documentNested, PropertyRule rule)
        {
            string validatorName;
            Severity? validationFailureSeverity;

            if (validationRules is ChildValidatorAdaptor childValidator)
            {
                validatorName = childValidator.ValidatorType.Name;
                validationFailureSeverity = childValidator.Severity;

                if(null == null)
                {
                    Type type = typeof(IValidator<>);
                    //Type[] typeArgs = { rule.Member.DeclaringType };
                    Type constructed = type.MakeGenericType(rule.Member.DeclaringType);

                    var methodInfo = typeof(ValiDoc).GetRuntimeMethods();//.("GetRules", new Type[] { typeof(IValidator<>), typeof(bool) });
                    //var genericMethod = methodInfo.MakeGenericMethod(rule.TypeToValidate);

                    MethodInfo myMethod = null;

                    using (IEnumerator<MethodInfo> enumer = methodInfo.GetEnumerator())
                    {
                        if (enumer.MoveNext()) myMethod = enumer.Current;
                    }


                    myMethod = myMethod.MakeGenericMethod(constructed);

                    myMethod.Invoke(null, new object[] { childValidator.GetValidator(new PropertyValidatorContext(new ValidationContext(rule.Member.DeclaringType), rule, propertyName)), true });


                    var childValidatorInstance = childValidator.GetValidator(new PropertyValidatorContext(new ValidationContext(rule.Member.DeclaringType), rule, propertyName));
                    if(childValidatorInstance != null)
                    {
                        //genericMethod.Invoke(childValidatorInstance, new object[] { true });

                        //return childValidatorInstance.GetRules<typeGeneric>(true);
                    }
                }
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
                FailureSeverity = validationFailureSeverity.ToString(),
                OnFailure = cascadeMode.ToString()
            };
        }
    }
}
