﻿using Microservice.Core;
using Microservice.Core.DomainService.Events;
using System;

namespace ezStore.DomainEvents.Product
{
    [MessageBusRoute(EventRouteConstants.ProductService)]
    public class ProductCategoryDeleted : DomainEvent
    {
        public Guid Id { get; set; }
        public ProductCategoryDeleted(Guid id)
        {
            Id = id;
        }
    }
}