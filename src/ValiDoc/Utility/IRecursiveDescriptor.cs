using FluentValidation.Internal;
using FluentValidation.Validators;
using System.Collections.Generic;
using ValiDoc.Core;
using ValiDoc.Output;

namespace ValiDoc.Utility
{
    public interface IRecursiveDescriptor
    {
        IEnumerable<RuleDescription> GetNestedRules(string propertyName, PropertyRule rule, ChildValidatorAdaptor childValidator, IRuleDescriptor ruleDescriptor);
    }
}
