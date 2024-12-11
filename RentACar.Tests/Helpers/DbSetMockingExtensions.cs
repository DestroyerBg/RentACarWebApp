using Microsoft.EntityFrameworkCore;
using Moq;

namespace RentACar.Tests.Helpers
{
    public static class DbSetMockingExtensions
    {
        public static Mock<DbSet<T>> ReturnsDbSet<T>(this Mock<DbSet<T>> mockDbSet, IEnumerable<T> sourceList) where T : class
        {
            var asyncQueryable = sourceList.AsAsyncQueryable();

            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(asyncQueryable.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(asyncQueryable.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(asyncQueryable.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => asyncQueryable.GetEnumerator());
            mockDbSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(() => asyncQueryable.GetEnumerator());

            return mockDbSet;
        }


    }
}
