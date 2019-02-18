using Microservices.FileSystem.ApplicationCore.Entities;
using Microservices.FileSystem.ApplicationCore.Interfaces;
using Microservices.FileSystem.ApplicationCore.Services.Commands;
using System.Threading.Tasks;
using Ws4vn.Microservices.ApplicationCore.Interfaces;

namespace Microservices.FileSystem.ApplicationCore.Services.CommandHandlers
{
    public class FileSystemCommandHandler : ICommandHandler<UploadFilesCommand>
    {
        private readonly IDomainService _domainService;
        private readonly IFSRepository _fsService;

        public FileSystemCommandHandler(IFSRepository fsService, IDomainService domainService)
        {
            _domainService = domainService;
            _fsService = fsService;
        }

        public async Task ExecuteAsync(UploadFilesCommand command)
        {
            foreach (var item in command.FileMetadatas)
            {
                var id = _fsService.UploadFile(item.Name, item.Data).Result;
                await Task.Run(() =>
                {
                    _domainService.WriteService.Repository<FileMetadata>().Insert(new FileMetadata { Name = item.Name, FileSystemId = id });
                });
            }
        }
    }
}
