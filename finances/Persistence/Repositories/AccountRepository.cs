using Finances.Domain;
using Finances.Persistence.Contracts.Repositories;
using Finances.Persistence.Data;

using Microsoft.EntityFrameworkCore;

namespace Finances.Persistence.Repositories;

public class AccountRepository: IAccountRepository
{
    private readonly FinancesContext _db;
    public AccountRepository(FinancesContext db)
    {
        _db = db;
    }

    public async Task CreateAccountAsync(Account account)
    {
        await _db.Accounts.AddAsync(account);
    }

    public void UpdateAccountAsync(Account account)
    {
        _db.Accounts.Update(account);
    }

    public async Task DeleteAccountAsync(int id)
    {
        Account account = await _db.Accounts.Include(x => x.Transactions).FirstOrDefaultAsync(x => x.Id == id);

        if (account is not null)
        {
            _db.Accounts.Remove(account);
            _db.Transactions.RemoveRange(account.Transactions);
        }
    }

    public async Task<Account?> GetAccountByIdAsync(int id)
    {
        return await _db.Accounts.Include(x => x.Transactions).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Account>> GetAccountsByUserIdAsync(int userId)
    {
        return await _db.Accounts.Include(x => x.Transactions).Where(a => a.UserId == userId).ToListAsync();
    }

    public async Task UpdateAccountBalanceAsync(int accountId, decimal amount)
    {
        Account account = await _db.Accounts.FindAsync(accountId);

        if (account is not null)
        {
            account.UpdateBalance(amount);
        }
    }
}
