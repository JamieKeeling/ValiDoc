using System;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System.Collections.Generic;
using System.Reflection;
using ValiDoc.Extensions;
using ValiDoc.Output;

namespace ValiDoc.Utility
{
	public static class RecursiveDocument
    {
	    public static IEnumerable<RuleDescription> GetNestedRules(string propertyName, PropertyRule rule, ChildValidatorAdaptor childValidator)
	    {
			//TODO: Pull this into a class, define interface and pass type into constructor
		    var coreDocumentationType = typeof(DocBuilder);
		    const string methodIdentifier = "Document";

		    var getRulesMethodDefinition = coreDocumentationType.ExtractMethodInfo(new[] { methodIdentifier })[methodIdentifier];

		    // Create the generic method instance of Document()
		    getRulesMethodDefinition = getRulesMethodDefinition.MakeGenericMethod(childValidator.ValidatorType.GetTypeInfo().BaseType.GenericTypeArguments[0]);

		    //Parameter 1 = Validator instance derived from AbstractValidator<T>, Parameter 2 = boolean (documentNested)
		    var parameterArray = new object[]
		    {
			    childValidator.GetValidator(FluentValidationHelpers.BuildPropertyValidatorContext(rule, propertyName)),
			    true
		    };

		    //Invoke extension method with validator instance
		    var documentationInstance = Activator.CreateInstance(coreDocumentationType);
		    var nestedRules = getRulesMethodDefinition.Invoke(documentationInstance, parameterArray) as IEnumerable<RuleDescription>;

		    if (nestedRules == null)
			    yield return null;

		    foreach (var deepDocumentRule in nestedRules)
		    {
			    yield return deepDocumentRule;
		    }
	    }
	}
}
