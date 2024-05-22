using System.Linq.Expressions;

using Finances.Domain;

namespace Finances.Persistence.Contracts.Repositories;

public interface ITransactionRepository
{
    public Task<Transaction?> GetTransactionAsync(int id);
    public Task<List<Transaction>> GetTransactionsAsync(Expression<Func<Transaction,bool>> exp);
    public Task CreateTransactionAsync(Transaction transaction);
    public void UpdateTransactionAsync(Transaction transaction);
    public Task DeleteTransactionAsync(int id);
}
