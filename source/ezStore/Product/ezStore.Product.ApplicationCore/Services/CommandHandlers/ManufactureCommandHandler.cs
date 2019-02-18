using ezStore.Product.ApplicationCore.Dtos;
using ezStore.Product.ApplicationCore.ProductAggregate;
using ezStore.Product.ApplicationCore.Services.Commands;
using System.Threading.Tasks;
using Ws4vn.Microservices.ApplicationCore.Interfaces;

namespace ezStore.Product.ApplicationCore.Services.CommandHandlers
{
    public class ManufactureCommandHandler : ICommandHandler<CreateManufactureCommand>
    {
        private readonly IDomainService _domainService;

        public ManufactureCommandHandler(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public Task ExecuteAsync(CreateManufactureCommand command)
        {
            var productCategoryDomain = new ManufactureDomain(_domainService.WriteService);
            productCategoryDomain.Add(new ManufactureDto
            {
                Name = command.Name
            });

            _domainService.ApplyChanges(productCategoryDomain);
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(UpdateManufactureCommand command)
        {
            var productCategoryDomain = new ManufactureDomain(_domainService.WriteService);
            productCategoryDomain.Update(new ManufactureDto
            {
                Id = command.Id,
                Name = command.Name
            });

            _domainService.ApplyChanges(productCategoryDomain);
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(DeleteManufactureCommand command)
        {
            var productCategoryDomain = new ManufactureDomain(_domainService.WriteService);
            productCategoryDomain.Delete(command.Id);

            _domainService.ApplyChanges(productCategoryDomain);
            return Task.CompletedTask;
        }
    }
}
