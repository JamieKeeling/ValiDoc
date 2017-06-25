using System.Collections.Generic;
using System.Reflection;
using FluentValidation.Internal;
using FluentValidation.Validators;
using ValiDoc.Extensions;
using ValiDoc.Output;
using ValiDoc.Utility;

namespace ValiDoc.Core
{
	public static class DocumentationBuilder
	{
		public static IEnumerable<RuleDescription> GetNestedRules(string propertyName, PropertyRule rule, ChildValidatorAdaptor childValidator)
		{
			const string methodIdentifier = "Document";

			var getRulesMethodDefinition = typeof(DocBuilder).ExtractMethodInfo(new[] { methodIdentifier })[methodIdentifier];

			// Create the generic method instance of Document()
			getRulesMethodDefinition = getRulesMethodDefinition.MakeGenericMethod(childValidator.ValidatorType.GetTypeInfo().BaseType.GenericTypeArguments[0]);

			//Parameter 1 = Validator instance derived from AbstractValidator<T>, Parameter 2 = boolean (documentNested)
			var parameterArray = new object[]
			{
				childValidator.GetValidator(FluentValidationHelpers.BuildPropertyValidatorContext(rule, propertyName)),
				true
			};

			//Invoke Document on the AbstractValidator<T> instance
			var nestedRules = getRulesMethodDefinition.Invoke(null, parameterArray) as IEnumerable<RuleDescription>;

			if (nestedRules == null)
				yield return null;

			foreach (var deepDocumentRule in nestedRules)
			{
				yield return deepDocumentRule;
			}
		}
	}
}
