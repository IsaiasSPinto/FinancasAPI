using ErrorOr;

using Finances.Models.Transactions;

namespace Finances.Services.Contracts;

public interface ITransactionService
{
    public Task<List<TransactionDto>> GetAllUserTransactionsAsync(int userId);
    public Task<TransactionDto?> GetTransactionAsync(int id);
    public Task<ErrorOr<int>> CreateTransactionAsync(CreateTransactionDto transaction);
    public Task<ErrorOr<TransactionDto>> UpdateTransactionAsync(UpdateTransactionDto transaction);
    public Task<ErrorOr<bool>> DeleteTransactionAsync(int id);
}
