using Microsoft.Extensions.Configuration;
using Moq;
using UserControl.Core.Abstractions.Repositories;
using UserControl.Core.Dtos.Phones;
using UserControl.Core.Dtos.Users;
using UserControl.Core.Exceptions;
using UserControl.Model.Entities;
using UserControl.Repository;
using UserControl.Services;
using UserControl.Tests.Mocks;

namespace UserControl.Tests.UserUnitTest;
public class UserTests
{
    private readonly BaseRepository<UserEntity> _mockUserRepository;
    private readonly BaseRepository<PhoneEntity> _phoneRepositoryMock;
    private readonly Mock<IConfiguration> _configurationMock;

    public UserTests()
    {
        _mockUserRepository = UserMocks.GetUserRepository(UserContactContextMock.Get());
        _phoneRepositoryMock = UserMocks.GetPhoneRepository(UserContactContextMock.Get());
        _configurationMock = new Mock<IConfiguration>();
    }

    public static List<PhoneDto> Phones { get; } =
    [
        new PhoneDto
        {
            Number = "8090001234",
            CityCode = "1",
            CountryCode = "50"
        },
    ];


    [Fact]
    public async Task ShouldRegisterUsers_WhenDataIsValid()
    {
        // Arrange
        var createUser = new CreateUserDto
        {
            Name = "Test User",
            Email = "waldis@example.com",
            Password = "Password123457@",
            Phones = Phones
        };

        var service = new UserService(_mockUserRepository, _phoneRepositoryMock, _configurationMock.Object);

        // Act
        var result = await service.RegisterUser(createUser, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UserResponseDto>(result);
    }

    [Fact]
    public async Task ShouldThrowException_WhenUserAlreadyExists()
    {
        // Arrange
        var createUser = new CreateUserDto
        {
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "Password123",
            Phones = Phones
        };

        var service = new UserService(_mockUserRepository, _phoneRepositoryMock, _configurationMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => service.RegisterUser(createUser, CancellationToken.None));
    }
}
