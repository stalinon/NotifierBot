using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using NotifierBot.Data.Entities;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Data.Impl.Repositories.Mock;

/// <summary>
///     Базовый мок репозитория
/// </summary>
internal class MockRepository<TEntity> : Repository<TEntity>
    where TEntity : class, IHasId
{
    /// <inheritdoc cref="MockRepository{TEntity}" />
    protected MockRepository(List<TEntity> entities) : base(SetupDbContext(entities))
    {
    }

    private static DbContext SetupDbContext(List<TEntity> entities)
    {
        var mock = new Mock<DbContext>();
        mock.Setup(c => c.Set<TEntity>()).Returns(entities.AsQueryable().BuildMock().BuildMockDbSet().Object);
        mock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns((CancellationToken _) => Task.FromResult(0));
        
        return mock.Object;
    }
    
    /// <inheritdoc />
    public override Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var items = DbSet.ToList();
        items.Add(SetIdentifierToEntity(entity, items));
        DbSet = items.BuildMock().BuildMockDbSet().Object;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public override Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var items = DbSet.ToList();
        foreach (var entity in items)
        {
            _ = SetIdentifierToEntity(entity, items);
        }

        items.AddRange(entities.Select(e => SetIdentifierToEntity(e, items)));
        DbSet = items.BuildMock().BuildMockDbSet().Object;
        return Task.CompletedTask;
    }

    private static TEntity SetIdentifierToEntity(TEntity entity, List<TEntity> items)
    {
        var maxId = items.Any() ? items.Max(i => i.Id) : 0;
        entity.Id = maxId != 0 ? maxId + 1 : 1;
        return entity;
    }
}