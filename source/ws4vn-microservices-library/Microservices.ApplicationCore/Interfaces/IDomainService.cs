using System.Threading.Tasks;
using Ws4vn.Microservices.ApplicationCore.Entities;

namespace Ws4vn.Microservices.ApplicationCore.Interfaces
{
    public interface IDomainService
    {
        IDataAccessWriteService WriteService { get; }

        void ApplyChanges<TEntity>(TEntity entity) where TEntity : AggregateRoot;

        Task ApplyChangesAsync<TEntity>(TEntity entity) where TEntity : AggregateRoot;
    }
}