﻿using System.Collections.Generic;
using FluentValidation;
using ValiDoc.Output;

namespace ValiDoc.Core
{
	public interface IDocumentRules
	{
		IEnumerable<RuleDescription> Document<T>(AbstractValidator<T> validator, bool includeNested = false);
	}
}