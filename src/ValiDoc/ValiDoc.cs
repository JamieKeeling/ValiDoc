using FluentValidation;
using FluentValidation.Internal;
using System;
using System.Collections.Generic;
using ValiDoc.Core;
using ValiDoc.Output;

namespace ValiDoc
{
	public static class ValiDoc
	{
		public static IEnumerable<RuleDescription> GetRules<T>(this AbstractValidator<T> validator, bool documentNested = false)
		{
			if (validator == null)
			{
				throw new ArgumentNullException(nameof(validator));
			}

			var descriptor = validator.CreateDescriptor();

			if (descriptor == null)
			{
				throw new ArgumentNullException(nameof(descriptor));
			}

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
						var documentedRules = RuleDescriptionBuilder.BuildRuleDescription(validationRules, propertyName, rule.CascadeMode, documentNested, rule);

						foreach (var documentedRule in documentedRules)
						{
							yield return documentedRule;
						}
					}
				}
			}
		}
	}
}
