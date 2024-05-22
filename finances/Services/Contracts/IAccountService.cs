using ErrorOr;
using Finances.Models.Account;

namespace Finances.Services.Contracts;

public interface IAccountService
{
    public Task<ErrorOr<AccountDto>> GetAccountByIdAsync(int id);

    public Task<List<AccountDto>> GetAccountsByUserIdAsync(int userId);

    public Task<ErrorOr<AccountDto>> CreateAccountAsync(CreateAccountDto createAccountDto);

    public Task<ErrorOr<AccountDto>> UpdateAccountAsync(UpdateAccountDto updateAccountDto);

    public Task DeleteAccountAsync(int id);
}
