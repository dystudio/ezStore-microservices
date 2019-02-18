using ezStore.Product.ApplicationCore.Services.Commands;
using ezStore.Product.ApplicationCore.ProductAggregate;
using Ws4vn.Microservices.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using ezStore.Product.ApplicationCore.Dtos;

namespace ezStore.Product.ApplicationCore.Services.CommandHandlers
{
    public class ProductCategoryCommandHandler : ICommandHandler<CreateProductCategoryCommand>,
        ICommandHandler<UpdateProductCategoryCommand>,
        ICommandHandler<DeleteProductCategoryCommand>
    {
        private readonly IDomainService _domainService;

        public ProductCategoryCommandHandler(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public Task ExecuteAsync(CreateProductCategoryCommand command)
        {
            var productCategoryDomain = new ProductCategoryDomain(_domainService.WriteService);
            productCategoryDomain.Add(new ProductCategoryDto
            {
                Name = command.Name
            });

            _domainService.ApplyChanges(productCategoryDomain);
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(UpdateProductCategoryCommand command)
        {
            var productCategoryDomain = new ProductCategoryDomain(_domainService.WriteService);
            productCategoryDomain.Update(new ProductCategoryDto
            {
                Id = command.Id,
                Name = command.Name
            });

            _domainService.ApplyChanges(productCategoryDomain);
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(DeleteProductCategoryCommand command)
        {
            var productCategoryDomain = new ProductCategoryDomain(_domainService.WriteService);
            productCategoryDomain.Delete(command.Id);

            _domainService.ApplyChanges(productCategoryDomain);
            return Task.CompletedTask;
        }
    }
}
