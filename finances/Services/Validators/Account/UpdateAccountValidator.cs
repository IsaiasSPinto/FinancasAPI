using Finances.Models.Account;

using FluentValidation;

namespace Finances.Services.Validators.Account;

public class UpdateAccountValidator : AbstractValidator<UpdateAccountDto>
{
    public UpdateAccountValidator()
    {
        Include(new CreateAccountValidator());
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);     
    }
}
