# ValiDoc

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

| MemberName        | ValidatorName           | FailureSeverity  | OnFailure |
| :-------------: |:-------------:| :-----:|:---------:|
| First Name      | NotEmptyValidator | Error | Continue |
| Last Name      | NotEmptyValidator      |   Error | Continue |
| Last Name | MaximumLengthValidator      |    Error | Continue |
| Address | AddressValidator | Error | Continue |

## 
#### Supported Scenarios

1. [Built in validators](https://github.com/JeremySkinner/FluentValidation/wiki/c.-Built-In-Validators)
2. [Chained validators for the same property](https://github.com/JeremySkinner/FluentValidation/wiki/b.-Creating-a-Validator#chaining-validators-for-the-same-property)
3. [Complex properties](https://github.com/JeremySkinner/FluentValidation/wiki/b.-Creating-a-Validator#complex-properties)
4. [Cascade behaviour](https://github.com/JeremySkinner/FluentValidation/wiki/d.-Configuring-a-Validator#setting-the-cascade-mode)
5. Nested chained validators and their associated validation rules


#### Future Roadmap

1. Collections
2. RuleSets
3. .WithMessage()
4. .WithName()
5. Validation arguments (Greater than 10 characters for example)
6. Custom validators
7. .Must()
