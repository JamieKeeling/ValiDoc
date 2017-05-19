using System;
using FluentValidation;
using ValiDoc.Tests.TestData;

namespace ValiDoc.Tests
{
	public class PersonValidator : AbstractValidator<Person>
	{
		public PersonValidator()
		{
			RuleFor(p => p.FirstName).NotEmpty();
			RuleFor(p => p.LastName).NotEmpty();
		}
	}
}
