using Microsoft.AspNetCore.Identity;

namespace Finances.Domain;

public class User : IdentityUser<int>
{
    public List<Account> Accounts { get; set; }
    public List<Transaction> Transactions { get; set; }
}
