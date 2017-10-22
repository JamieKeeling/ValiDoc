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
	    private readonly IRuleDescriptor _ruleDescriptor;

	    public DocBuilder(IRuleDescriptor ruleDescriptor)
	    {
	        _ruleDescriptor = ruleDescriptor;
	    }

		public IEnumerable<RuleDescription> Document<T>(AbstractValidator<T> validator)
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

					foreach (var validationRules in rule.Validators)
					{
						foreach (var documentedRule in _ruleDescriptor.BuildRuleDescription(validationRules, propertyName, rule.CascadeMode, rule))
						{
							yield return documentedRule;
						}
					}
				}
			}
		}
	}
}