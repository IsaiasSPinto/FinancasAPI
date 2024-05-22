using Finances.Models.Transactions;

namespace Finances.Models.Account;

public class AccountDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public List<TransactionDto> Transactions { get; set; }
}
