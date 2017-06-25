using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Internal;
using ValiDoc.Core;
using ValiDoc.Output;

namespace ValiDoc
{
	public class DocBuilder : IDocumentRules
	{
		public IEnumerable<RuleDescription> Document<T>(AbstractValidator<T> validator, bool includeChildValidators = false)
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
						foreach (var documentedRule in RuleDescriptionBuilder.BuildRuleDescription(validationRules, propertyName, rule.CascadeMode, includeChildValidators, rule))
						{
							yield return documentedRule;
						}
					}
				}
			}
		}
	}
}