﻿using System.Net;
using Microsoft.EntityFrameworkCore;
using NotifierBot.Business.Impl.Mapping;
using NotifierBot.Business.Services.DataManagement;
using NotifierBot.Data;
using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;
using NotifierBot.Infrastructure.Maintenance.Exceptions;
using NotifierBot.Infrastructure.Models.Abstractions;

namespace NotifierBot.Business.Impl.Services.DataManagement;

/// <inheritdoc cref="IDataManager{TModel}" />
internal abstract class DataManager<TEntity, TModel> : IDataManager<TModel>
    where TModel : class, IModelWithId, new()
    where TEntity : class, IHasId, new()
{
    private readonly Mapper<TEntity, TModel> _mapper;
    private readonly IDataAccessFacade _database;
    
    protected abstract Func<IDataAccessFacade, IRepository<TEntity>> RepositorySelector { get; }

    /// <inheritdoc cref="DataManager{TEntity, TModel}" />
    protected DataManager(Mapper<TEntity, TModel> mapper, IDataAccessFacade database)
    {
        _mapper = mapper;
        _database = database;
    }
    
    /// <inheritdoc />
    public virtual async Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map(model);

        SetAutomaticProps(entity);
        await RepositorySelector(_database).AddAsync(entity, cancellationToken);
        await RepositorySelector(_database).SaveChangesAsync(cancellationToken);

        return _mapper.Map(entity);
    }

    /// <inheritdoc />
    public virtual async Task<TModel?> ReadAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await RepositorySelector(_database).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (entity == null)
        {
            throw new ErrorException(HttpStatusCode.NotFound, $"Сущность #{id} не была найдена.");
        }

        return _mapper.Map(entity);
    }

    /// <inheritdoc />
    public virtual async Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        var entity = await RepositorySelector(_database).FirstOrDefaultAsync(e => e.Id == model.Id, cancellationToken);

        if (entity == null)
        {
            throw new ErrorException(HttpStatusCode.NotFound, $"Сущность #{model.Id} не была найдена.");
        }

        SetAutomaticProps(entity);
        _ = _mapper.Map(model, entity);
        await RepositorySelector(_database).SaveChangesAsync(cancellationToken);

        return model;
    }

    /// <inheritdoc />
    public virtual async Task<TModel> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await RepositorySelector(_database).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (entity == null)
        {
            throw new ErrorException(HttpStatusCode.NotFound, $"Сущность #{id} не была найдена.");
        }

        RepositorySelector(_database).Remove(entity);
        await RepositorySelector(_database).SaveChangesAsync(cancellationToken);

        return _mapper.Map(entity);
    }

    /// <inheritdoc />
    public virtual async Task<TModel[]> ListAsync(long? limit = null, long? offset = null, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = RepositorySelector(_database).CreateQuery().OrderByDescending(e => e.Id);
        if (offset.HasValue)
        {
            query = query.Skip((int)offset.Value);
        }

        if (limit.HasValue)
        {
            query = query.Take((int)limit.Value);
        }

        var list = await query.ToArrayAsync(cancellationToken);
        return _mapper.Map(list);
    }

    private void SetAutomaticProps(TEntity entity)
    {
        if (entity is IEntityLifecycle lifecycle)
        {
            lifecycle.SetLifecycle();
        }
    }
}