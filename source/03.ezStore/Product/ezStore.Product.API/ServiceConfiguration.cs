﻿using ezStore.Product.Domain.Application.Queries;
using ezStore.Product.Infrastructure;
using MassTransit;
using MassTransit.Util;
using Microservice.Core;
using Microservice.Core.DomainService;
using Microservice.Core.DomainService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microservice.DataAccess.Core.Interfaces;
using Microservice.DataAccess.Sql;

namespace ezStore.Product.API
{
    public static class ServiceConfiguration
    {
        private static IBusControl _bus;

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(i =>
            {
                if (_bus == null)
                {
                    _bus = Bus.Factory.CreateUsingRabbitMq(x =>
                    {
                        var username = configuration.GetConnectionString(MicroserviceConstants.RabbitMQUsername);
                        var password = configuration.GetConnectionString(MicroserviceConstants.RabbitMQPassword);
                        var host = configuration.GetConnectionString(MicroserviceConstants.RabbitMQHost);
                        if (!string.IsNullOrEmpty(host))
                        {
                            x.Host(new Uri(host), h =>
                            {
                                if (!string.IsNullOrEmpty(username))
                                {
                                    h.Username(configuration.GetConnectionString(MicroserviceConstants.RabbitMQUsername));
                                }
                                if (!string.IsNullOrEmpty(password))
                                {
                                    h.Password(configuration.GetConnectionString(MicroserviceConstants.RabbitMQPassword));
                                }

                            });
                        }
                    });
                    TaskUtil.Await(() => _bus.StartAsync());
                }
                return _bus;
            });

            services.AddScoped<IValidationContext, ValidationContext>();
            services.AddTransient<ICommandProcessor, CommandProcessor>();

            // Add application services.
            services.AddTransient<IProductCategoryQueries, ProductCategoryQueries>();
            services.AddTransient<IDomainContext>(i => new DomainContext(i.GetService<IConfiguration>(), i.GetService<IBusControl>()));
            services.AddTransient<IDataAccessService>(i => new DataAccessWriteService(i.GetService<ProductDbContext>()));
            services.AddTransient<IDataAccessWriteService>(i => new DataAccessWriteService(i.GetService<ProductDbContext>()));
            services.AddTransient<IDomainService>(i => new DomainService(i.GetService<IDomainContext>(), i.GetService<IDataAccessWriteService>()));
            services.AddTransient<IDataAccessReadOnlyService>(i => new DataAccessReadOnlyService(i.GetService<ProductDbContext>()));

            Domain.HandlerRegister.Register(services);
        }
    }
}
