using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit(CancellationToken cancellationToken);
    }
}
