using System.Linq.Expressions;

using Finances.Domain;
using Finances.Persistence.Contracts.Repositories;
using Finances.Persistence.Data;

using Microsoft.EntityFrameworkCore;

namespace Finances.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly FinancesContext _db;
    public TransactionRepository(FinancesContext db)
    {
        _db = db;
    }

    public async Task<Transaction?> GetTransactionAsync(int id)
    {
        return await _db.Transactions.FindAsync(id);
    }

    public async Task<List<Transaction>> GetTransactionsAsync(Expression<Func<Transaction,bool>> exp)
    {
        return await _db.Transactions.Where(exp).ToListAsync();
    }

    public async Task CreateTransactionAsync(Transaction transaction)
    {
        await _db.Transactions.AddAsync(transaction);
    }

    public void UpdateTransactionAsync(Transaction transaction)
    {
        _db.Transactions.Update(transaction);
    }

    public async Task DeleteTransactionAsync(int id)
    {
        Transaction tran = await _db.Transactions.FindAsync(id);

        if (tran is not null)
        {
            _db.Transactions.Remove(tran);
        }
    }
}
