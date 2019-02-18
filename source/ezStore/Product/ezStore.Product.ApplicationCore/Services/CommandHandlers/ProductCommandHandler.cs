using ezStore.Product.ApplicationCore.Dtos;
using ezStore.Product.ApplicationCore.ProductAggregate;
using ezStore.Product.ApplicationCore.Services.Commands;
using System.Threading.Tasks;
using Ws4vn.Microservices.ApplicationCore.Interfaces;

namespace ezStore.Product.ApplicationCore.Services.CommandHandlers
{
    public class ProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IDomainService _domainService;

        public ProductCommandHandler(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task ExecuteAsync(CreateProductCommand command)
        {
            var productCategoryDomain = new ProductDomain(_domainService.WriteService);
            productCategoryDomain.Add(new ProductDto
            {
                Name = command.Name
            });

            await _domainService.ApplyChangesAsync(productCategoryDomain);
        }

        public async Task ExecuteAsync(UpdateProductCommand command)
        {
            var productCategoryDomain = new ProductDomain(_domainService.WriteService);
            productCategoryDomain.Update(new ProductDto
            {
                Id = command.Id,
                Name = command.Name
            });

            await _domainService.ApplyChangesAsync(productCategoryDomain);
        }

        public async Task ExecuteAsync(DeleteProductCommand command)
        {
            var productCategoryDomain = new ProductDomain(_domainService.WriteService);
            productCategoryDomain.Delete(command.Id);

            await _domainService.ApplyChangesAsync(productCategoryDomain);
        }
    }
}
