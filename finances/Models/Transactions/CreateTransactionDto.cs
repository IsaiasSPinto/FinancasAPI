using Finances.Domain;

namespace Finances.Models.Transactions;

public class CreateTransactionDto
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public int AccountId { get; set; }
}
