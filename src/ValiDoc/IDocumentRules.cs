using System.Collections.Generic;
using FluentValidation;
using ValiDoc.Output;

namespace ValiDoc
{
	public interface IDocumentRules
	{
		IEnumerable<RuleDescriptor> Document<T>(AbstractValidator<T> validator);
	}
}