using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using Moq.Language.Flow;

namespace EfQueries.Tests
{
    public static class MockDbContextExtensions
    {
        public static void Returns<TContext, TEntity>(this ISetup<TContext, DbSet<TEntity>> dbSet,
            IEnumerable<TEntity> collection)
            where TContext : DbContext
            where TEntity : class
        {
            var queryableCollection = collection.AsQueryable();
            var mockDbSet = new Mock<DbSet<TEntity>>();

            mockDbSet.As<IQueryable<TEntity>>().Setup(x => x.ElementType).Returns(queryableCollection.ElementType);
            mockDbSet.As<IQueryable<TEntity>>().Setup(x => x.Expression).Returns(queryableCollection.Expression);
            mockDbSet.As<IQueryable<TEntity>>().Setup(x => x.Provider).Returns(queryableCollection.Provider);
            mockDbSet.As<IQueryable<TEntity>>().Setup(x => x.GetEnumerator())
                .Returns(() => queryableCollection.GetEnumerator());

            mockDbSet.Setup(x => x.AsNoTracking()).Returns(mockDbSet.Object);

            dbSet.Returns(mockDbSet.Object);
        }
    }
}
