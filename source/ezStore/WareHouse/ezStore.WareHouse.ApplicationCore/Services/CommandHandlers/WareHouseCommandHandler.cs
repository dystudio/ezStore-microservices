using ezStore.WareHouse.ApplicationCore.Services.Commands;
using ezStore.WareHouse.ApplicationCore.WareHouseAggregate;
using System.Threading.Tasks;
using Ws4vn.Microservices.ApplicationCore.Interfaces;

namespace ezStore.WareHouse.ApplicationCore.Services.CommandHandlers
{
    public class WareHouseCommandHandler : ICommandHandler<CreateWareHouseCommand>,
        ICommandHandler<UpdateWareHouseCommand>,
        ICommandHandler<DeleteWareHouseCommand>
    {
        private readonly IDomainService _domainService;

        public WareHouseCommandHandler(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public Task ExecuteAsync(CreateWareHouseCommand command)
        {
            var wareHouseDomain = new WareHouseDomain(_domainService.WriteService);
            wareHouseDomain.CreateWareHouse(command.Name, command.CountryId, command.ProvinceId, command.Address, command.City, command.PhoneNumber, command.PostalCode);

            _domainService.ApplyChanges(wareHouseDomain);

            return Task.CompletedTask;
        }

        public Task ExecuteAsync(UpdateWareHouseCommand command)
        {
            var wareHouseDomain = new WareHouseDomain(_domainService.WriteService);
            wareHouseDomain.UpdateWareHouse(command.Id, command.Name);

            _domainService.ApplyChanges(wareHouseDomain);
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(DeleteWareHouseCommand command)
        {
            var wareHouseDomain = new WareHouseDomain(_domainService.WriteService);
            wareHouseDomain.DeleteWareHouse(command.Id);

            _domainService.ApplyChanges(wareHouseDomain);
            return Task.CompletedTask;
        }
    }
}
