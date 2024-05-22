namespace Finances.Persistence.Contracts;

public interface IUnitOfWork : IDisposable
{
    public Task<int> CommitAsync(CancellationToken cancellationToken = default);

}
