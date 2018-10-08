﻿using Microservice.Core.DomainService.Commands;

namespace ezStore.Product.Domain.Application.Commands
{
    public class CreateProductCategoryCommand : ValidationDecoratorCommand
    {
        public string Name { get; set; }

        public CreateProductCategoryCommand(string name) : base(new NameValidatorCommand(name))
        {
            this.Name = name;
        }

        public override bool SelfValidate()
        {
            return true;
        }
    }
}
