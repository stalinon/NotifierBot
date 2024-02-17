using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Data.Impl.Repositories;

/// <inheritdoc />
internal abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IHasId
{
    private DbContext DbContext { get; }

    protected DbSet<TEntity> DbSet;

    /// <inheritdoc cref="Repository{TEntity}"/>
    protected Repository(DbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    /// <inheritdoc />
    public virtual IQueryable<TEntity> CreateQuery(Expression<Func<TEntity, bool>>? filter = null) => filter == null
        ? DbSet.AsQueryable()
        : DbSet.AsQueryable().Where(filter);

    /// <inheritdoc />
    public virtual async Task<TEntity[]> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>? sort = null,
        ColumnSortType sortDirection = ColumnSortType.ASCENDING,
        CancellationToken cancellationToken = default)
    {
        var query = CreateQuery(filter);
        if (sort != null)
        {
            query = sortDirection switch
            {
                ColumnSortType.ASCENDING => query.OrderBy(sort),
                ColumnSortType.DESCENDING => query.OrderByDescending(sort),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return await query.ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default) =>
        await CreateQuery(filter).FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc />
    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is IEntityLifecycle timestamped)
        {
            timestamped.SetLifecycle();
        }
        
        await DbSet.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var hasIds = entities as TEntity[] ?? entities.ToArray();
        foreach (var entity in hasIds)
        {
            if (entity is IEntityLifecycle timestamped)
            {
                timestamped.SetLifecycle();
            }
        }
        
        await DbSet.AddRangeAsync(hasIds, cancellationToken);
    }

    /// <inheritdoc />
    public virtual void Remove(TEntity entity) => DbSet.Remove(entity);

    /// <inheritdoc />
    public virtual void RemoveRange(TEntity[] entities) => DbSet.RemoveRange(entities);

    /// <inheritdoc />
    public virtual IQueryable<TEntity> NoTracking() =>
        DbSet.AsNoTracking();

    /// <inheritdoc />
    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await DbContext.SaveChangesAsync(cancellationToken);
}