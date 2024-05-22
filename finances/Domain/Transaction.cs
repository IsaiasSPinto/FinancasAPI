namespace Finances.Domain;

public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }
    public Account Account { get; set; }
    public int AccountId { get; set; }
}
