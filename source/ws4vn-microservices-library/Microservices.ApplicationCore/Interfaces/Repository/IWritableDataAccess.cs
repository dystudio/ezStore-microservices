using System.Threading.Tasks;

namespace Ws4vn.Microservices.ApplicationCore.Interfaces
{
    public interface IWritableDataAccess
    {
        void SaveChanges();

        Task SaveChangesAsync();
    }
}