using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NotifierBot.Data.Entities;

namespace NotifierBot.Data.Impl.Repositories.Mock;

/// <summary>
///     Базовый мок репозитория
/// </summary>
internal class MockRepository<TEntity> : Repository<TEntity>
    where TEntity : class, IHasId
{
    /// <inheritdoc cref="MockRepository{TEntity}" />
    public MockRepository(List<TEntity> entities) : base(SetupDbContext(entities))
    { }

    private static DbContext SetupDbContext(List<TEntity> entities)
    {
        var mock = new Mock<DbContext>();
        mock.Setup(c => c.Set<TEntity>()).ReturnsDbSet(entities);
        return mock.Object;
    }
}