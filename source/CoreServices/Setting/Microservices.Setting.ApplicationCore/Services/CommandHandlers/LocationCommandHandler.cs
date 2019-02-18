using Microservices.Setting.ApplicationCore.Services.Commands;
using Microservices.Setting.ApplicationCore.SettingAggregate;
using Ws4vn.Microservices.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Microservices.Setting.ApplicationCore.Services.CommandHandlers
{
    public class LocationCommandHandler : ICommandHandler<CreateCountryCommand>,
        ICommandHandler<UpdateCountryCommand>,
        ICommandHandler<DeleteCountryCommand>
    {
        private readonly IDomainService _domainService;

        public LocationCommandHandler(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public Task ExecuteAsync(CreateCountryCommand command)
        {
            var locationDomain = new LocationDomain(_domainService.WriteService);
            locationDomain.CreateCountry(command.Name, command.IsoCode, command.DisplayOrder, command.Published);

            _domainService.ApplyChanges(locationDomain);
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(UpdateCountryCommand command)
        {
            var locationDomain = new LocationDomain(_domainService.WriteService);
            locationDomain.UpdateCountry(command.Id, command.Name, command.IsoCode, command.DisplayOrder, command.Published);

            _domainService.ApplyChanges(locationDomain);
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(DeleteCountryCommand command)
        {
            var locationDomain = new LocationDomain(_domainService.WriteService);
            locationDomain.DeleteCountry(command.Id);

            _domainService.ApplyChanges(locationDomain);
            return Task.CompletedTask;
        }
    }
}
