using System.Threading.Tasks;
using Ws4vn.Microservices.ApplicationCore.Entities;
using Ws4vn.Microservices.ApplicationCore.Interfaces;

namespace Ws4vn.Microservices.ApplicationCore.Services
{
    public class DomainService : IDomainService
    {
        private IDomainContext _context { get; }
        public IDataAccessWriteService WriteService { get; }

        public DomainService(IDomainContext context, IDataAccessWriteService writeService)
        {
            _context = context;
            WriteService = writeService;
        }

        public void ApplyChanges<TEntity>(TEntity entity) where TEntity : AggregateRoot
        {
            if (WriteService is IWritableDataAccess)
            {
                (WriteService as IWritableDataAccess).SaveChanges();
            }
            _context.SaveEvents(entity);
        }


        public async Task ApplyChangesAsync<TEntity>(TEntity entity) where TEntity : AggregateRoot
        {
            if (WriteService is IWritableDataAccess)
            {
                await (WriteService as IWritableDataAccess).SaveChangesAsync();
            }
            _context.SaveEvents(entity);
        }
    }
}