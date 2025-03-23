using Microsoft.EntityFrameworkCore;
using UserControl.Model.Context;

namespace UserControl.Tests;

public static class UserContactContextMock
{
    public static UserContactDbContext Get()
    {
        var options = new DbContextOptionsBuilder<UserContactDbContext>()
            .UseInMemoryDatabase(databaseName: $"UserContactDbContext-{Guid.NewGuid()}")
            .Options;

        var contextFake = new UserContactDbContext(options);
        contextFake.Database.EnsureDeleted();

        return contextFake;
    }
}
