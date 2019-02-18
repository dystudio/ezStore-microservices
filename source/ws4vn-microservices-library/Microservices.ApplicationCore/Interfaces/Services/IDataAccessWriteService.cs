﻿namespace Ws4vn.Microservices.ApplicationCore.Interfaces
{
    public interface IDataAccessWriteService 
    {
        IDataAccessWriteRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}