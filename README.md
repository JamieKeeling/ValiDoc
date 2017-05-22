# ValiDoc

An unobtrusive extension to FluentValidations for documenting validation rules.

##

#### Usage

1. Install ValiDoc via NuGet

2) Reference ValiDoc namespace in the same class as your validator usage

3) Invoke the exposed extension method on any AbstractValidation&lt;T&gt; instance

4) Examine the IEnumerable&lt;RuleDescription&gt; output
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

| MemberName        | ValidatorName           | FailureSeverity  |
| :-------------: |:-------------:| :-----:|
| First Name      | NotEmptyValidator | Error |
| Last Name      | NotEmptyValidator      |   Error |
| Last Name | MaximumLengthValidator      |    Error |
| Address | AddressValidator | Error |
