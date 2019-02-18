using Ws4vn.Microservices.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace Ws4vn.Microservices.ApplicationCore.Entities
{
    public class AggregateRoot : ModelGuidIdEntity
    {
        protected readonly IDataAccessWriteService _dataAccessService;

        public AggregateRoot(IDataAccessWriteService dataAccessService)
        {
            _dataAccessService = dataAccessService;
            Events = new List<IEvent>();
        }

        public List<IEvent> Events { get; private set; }

        protected void AddEvent(IEvent @event)
        {
            Events.Add(@event);
        }
    }
}