using Finances.Domain;

namespace Finances.Persistence.Contracts.Repositories;

public interface IAccountRepository
{
    public Task<Account?> GetAccountByIdAsync(int id);

    public Task<List<Account>> GetAccountsByUserIdAsync(int userId);

    public Task CreateAccountAsync(Account account);

    public void UpdateAccountAsync(Account account);

    public Task DeleteAccountAsync(int id);

    public Task UpdateAccountBalanceAsync(int accountId, decimal amount);

}
