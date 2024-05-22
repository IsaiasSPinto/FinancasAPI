using Finances.Persistence.Contracts;

namespace Finances.Persistence.Data;

public class UnitOfWork : IUnitOfWork
{

    private readonly FinancesContext _context;

    public UnitOfWork(FinancesContext context)
    {
        _context = context;
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
