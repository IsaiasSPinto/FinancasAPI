namespace Finances.Domain;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public List<Transaction> Transactions { get; set; }

    public static Account Create(string name, int userId)
    {
        return new Account
        {
            Name = name,
            UserId = userId,
            Balance = 0,
            Transactions = new List<Transaction>()
        };
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void UpdateBalance(decimal amount)
    {
        Balance += amount;
    }
}
