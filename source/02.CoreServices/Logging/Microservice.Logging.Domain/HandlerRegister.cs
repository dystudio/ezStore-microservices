﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservice.Logging.Domain
{
    public static class HandlerRegister
    {
        public static void Register(IServiceCollection services)
        {
            Microservice.Core.DomainService.HandlerRegister.Register(Assembly.GetExecutingAssembly(), services);
        }
    }
}