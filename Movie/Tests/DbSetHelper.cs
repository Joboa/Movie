using Microsoft.EntityFrameworkCore;
using Moq;

public static class DbSetHelper
{
    public static Mock<DbSet<T>> GetDbSetMock<T>(IEnumerable<T> entities) where T : class
    {
        var queryableEntities = entities.AsQueryable();

        var dbSetMock = new Mock<DbSet<T>>();

        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableEntities.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableEntities.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableEntities.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableEntities.GetEnumerator());

        return dbSetMock;
    }
}
