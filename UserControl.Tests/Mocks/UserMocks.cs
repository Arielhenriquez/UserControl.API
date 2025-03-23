using AutoFixture;
using UserControl.Model.Context;
using UserControl.Model.Entities;
using UserControl.Repository;

namespace UserControl.Tests.Mocks;

public class UserMocks
{
    public static BaseRepository<UserEntity> GetUserRepository(UserContactDbContext contextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var users = fixture.CreateMany<UserEntity>().ToList();

        users.Add(fixture.Build<UserEntity>()
            .With(ur => ur.Id, new Guid("93890089-6103-42bf-90a7-4a55535dc507"))
            .With(ur => ur.Email, "testuser@example.com")
            .Create());

        contextFake.Users.AddRange(users);
        contextFake.SaveChanges();

        return new BaseRepository<UserEntity>(contextFake);
    }

    public static BaseRepository<PhoneEntity> GetPhoneRepository(UserContactDbContext contextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var phones = fixture.CreateMany<PhoneEntity>().ToList();

        phones.Add(fixture.Build<PhoneEntity>()
            .With(ur => ur.Id, new Guid("17820330-9cd6-4d79-b7de-7410a221dcc1"))
            .With(ur => ur.PhoneNumber, "8095551234")
            .Create());

        contextFake.Phones.AddRange(phones);
        contextFake.SaveChanges();

        return new BaseRepository<PhoneEntity>(contextFake);
    }

}
