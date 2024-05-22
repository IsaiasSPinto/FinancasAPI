namespace Finances.Domain;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Account> Accounts { get; set; }
    public int AccountId { get; set; }
    public List<Transaction> Transactions { get; set; }
}
