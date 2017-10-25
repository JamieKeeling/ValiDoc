using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System.Collections.Generic;
using ValiDoc.Output;

namespace ValiDoc.Core
{
    public interface IRuleBuilder
    {
        RuleDescriptor BuildRuleDescription(IEnumerable<IPropertyValidator> validationRules, string propertyName, CascadeMode cascadeMode, PropertyRule rule);
    }
}
