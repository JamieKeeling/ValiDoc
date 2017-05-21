﻿using FluentValidation;
using ValiDoc.Tests.TestData.POCOs;

namespace ValiDoc.Tests.TestData.Validators
{
    public class MultipleRuleValidator : AbstractValidator<Person>
	{
		public MultipleRuleValidator()
		{
			RuleFor(p => p.FirstName).NotEmpty();
			RuleFor(p => p.LastName).NotEmpty().MaximumLength(20);
		}
	}
}