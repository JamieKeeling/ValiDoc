using FluentValidation;
using FluentValidation.Internal;
using System;
using System.Collections.Generic;
using ValiDoc.Core;
using ValiDoc.Output;

namespace ValiDoc
{
    public class DocBuilder : IDocumentRules
	{
	    private readonly IRuleBuilder _ruleBuilder;

	    public DocBuilder(IRuleBuilder ruleBuilder)
	    {
	        _ruleBuilder = ruleBuilder;
	    }

		public IEnumerable<RuleDescriptor> Document<T>(AbstractValidator<T> validator)
		{
			if (validator == null)
			{
				throw new ArgumentNullException(nameof(validator));
			}

			var descriptor = validator.CreateDescriptor();
            
			var memberValidators = descriptor.GetMembersWithValidators();

			foreach (var member in memberValidators)
			{
				var rules = descriptor.GetRulesForMember(member.Key);

				foreach (var validationRule in rules)
				{
					var rule = (PropertyRule)validationRule;
					var propertyName = rule.GetDisplayName();

				    yield return _ruleBuilder.BuildRuleDescription(rule.Validators, propertyName, rule.CascadeMode, rule);
                }
            }
		}
	}
}