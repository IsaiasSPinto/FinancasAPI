using Finances.Models.Transactions;

using FluentValidation;

namespace Finances.Services.Validators;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionDto>
{
    public CreateTransactionValidator()
    {
        RuleFor(t => t.Amount).GreaterThan(0);
        RuleFor(t => t.Description).NotEmpty().NotNull().MaximumLength(20);
        RuleFor(t => t.Date).NotEmpty();
        RuleFor(t => t.Type).IsInEnum();
    }
}
