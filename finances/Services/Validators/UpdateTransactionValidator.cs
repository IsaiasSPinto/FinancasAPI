using Finances.Models.Transactions;

using FluentValidation;

namespace Finances.Services.Validators;

public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionDto>
{
    public UpdateTransactionValidator()
    {
        Include(new CreateTransactionValidator());
        RuleFor(t => t.Id).NotEmpty().NotNull().GreaterThan(0);
    }
}
