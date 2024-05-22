using Finances.Models.Account;

using FluentValidation;

namespace Finances.Services.Validators.Account;

public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
{
    public CreateAccountValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
    }
}
