# ValiDoc [![Build Status](https://travis-ci.org/JamieKeeling/ValiDoc.svg?branch=master)](https://travis-ci.org/JamieKeeling/ValiDoc)

An unobtrusive extension to FluentValidations for documenting validation rules.

##

#### Usage

1. Install ValiDoc via NuGet

```<language>
Install-Package ValiDoc -Pre
```

2. Reference ValiDoc namespace in the same class as your validator usage

```csharp
using ValiDoc;
```

3. Invoke the exposed extension method on any AbstractValidation&lt;T&gt; instance

4. Examine the IEnumerable&lt;RuleDescription&gt; output

##

#### Example

##### Implementation of AbstractValidator&lt;T&gt;

```csharp
public class MultipleRuleSingleChildValidator : AbstractValidator<Person>
{
	public MultipleRuleSingleChildValidator()
	{
	    RuleFor(p => p.FirstName).NotEmpty();
	    RuleFor(p => p.LastName).NotEmpty().MaximumLength(20);
            RuleFor(p => p.Address).SetValidator(new AddressValidator());
	}
}
```
  
##### Invoking GetRules()

```csharp
public static IEnumerable<RuleDescription> GetValidationRules <T>(AbstractValidator<T> validator)
{
    return validator.GetRules();
}
```


##### Output

| MemberName        | ValidatorName           | FailureSeverity  | OnFailure | ValidationMessage
| :-------------: |:-------------:| :-----:|:---------:|:---------:| 
| First Name      | NotEmptyValidator | Error | Continue | 'First Name' should not be empty.
| Last Name      | NotEmptyValidator      |   Error | Continue | 'Last Name' should not be empty.
| Last Name | MaximumLengthValidator      |    Error | Continue | 'Last Name' must be between \{MinLength} and \{MaxLength} characters. You entered \{TotalLength} characters.
| Address | AddressValidator | Error | Continue |

## 
#### Supported Scenarios

1. Nested chained validators and their associated validation rules
2. Extraction of validation messages (Validation arguments not currently included
3. [Built in validators](https://github.com/JeremySkinner/FluentValidation/wiki/c.-Built-In-Validators)
4. [Chained validators for the same property](https://github.com/JeremySkinner/FluentValidation/wiki/b.-Creating-a-Validator#chaining-validators-for-the-same-property)
5. [Complex properties](https://github.com/JeremySkinner/FluentValidation/wiki/b.-Creating-a-Validator#complex-properties)
6. [Cascade behaviour](https://github.com/JeremySkinner/FluentValidation/wiki/d.-Configuring-a-Validator#setting-the-cascade-mode)


#### Future Roadmap

1. Collections
2. RuleSets
3. .WithMessage()
4. .WithName()
5. Validation arguments (Greater than 10 characters for example)
6. Custom validators
7. .Must()
