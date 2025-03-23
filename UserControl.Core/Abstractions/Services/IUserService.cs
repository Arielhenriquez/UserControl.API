using UserControl.Core.Dtos.Users;

namespace UserControl.Core.Abstractions.Services;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllUsers();
    Task<UserResponseDto> GetUserById(Guid id, CancellationToken cancellationToken);
    Task<UserResponseDto> RegisterUser(CreateUserDto createUserDto, CancellationToken cancellationToken);
    Task<string> LoginUser(UserLoginDto loginDto, CancellationToken cancellationToken);
    Task<ActiveUserDto> SetUserActiveStatus(Guid userId, bool isActive, CancellationToken cancellationToken);
    Task<UserResponseDto> UpdateUser(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    Task DeleteUser(Guid id, CancellationToken cancellationToken);

}