using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System.Collections.Generic;
using ValiDoc.Output;

namespace ValiDoc.Core
{
    public interface IRuleDescriptor
    {
        IEnumerable<RuleDescription> BuildRuleDescription(IPropertyValidator validationRules, string propertyName, CascadeMode cascadeMode, PropertyRule rule);
    }
}
