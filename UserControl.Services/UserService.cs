using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserControl.Core.Abstractions.Repositories;
using UserControl.Core.Abstractions.Services;
using UserControl.Core.Dtos.Users;
using UserControl.Core.Exceptions;
using UserControl.Model.Entities;
using UserControl.Repository.Projections;

namespace UserControl.Services;


//Agregar logica de isActive
public class UserService : IUserService
{
    private readonly IBaseRepository<UserEntity> _userRepository;
    private readonly IBaseRepository<PhoneEntity> _phoneRepository;
    private readonly IConfiguration _configuration;

    public UserService(IBaseRepository<UserEntity> userRepository, IBaseRepository<PhoneEntity> phoneRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _phoneRepository = phoneRepository;
        _configuration = configuration;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
    {
        var userDtos = await _userRepository.Query()
            .Select(UserProjections.UserToUserResponseDto)
            .ToListAsync();

        return userDtos;
    }

    public async Task<UserResponseDto> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetById(id, cancellationToken);
        return UserProjections.UserToUserResponseDto.Compile().Invoke(userEntity);
    }


    public async Task<UserResponseDto> RegisterUser(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var userEntity = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            Password = createUserDto.Password,
            LastLogin = DateTimeOffset.UtcNow,
            IsActive = true,
        };


        if (await EmailExists(createUserDto.Email))
        {
            throw new BadRequestException("El correo ya esta registrado");
        }

        await _userRepository.AddAsync(userEntity, cancellationToken);

        foreach (var phoneDto in createUserDto.Phones)
        {
            var phoneEntity = new PhoneEntity
            {
                Id = Guid.NewGuid(),
                PhoneNumber = phoneDto.Number,
                CityCode = phoneDto.CityCode,
                CountryCode = phoneDto.CountryCode,
                UserId = userEntity.Id,
            };

            await _phoneRepository.AddAsync(phoneEntity, cancellationToken);
        }

        var userResponseDto = await _userRepository.Query()
            .Where(u => u.Id == userEntity.Id)
            .Select(UserProjections.UserToUserResponseDto)
            .FirstOrDefaultAsync();

        return userResponseDto;
    }

    public async Task<string> LoginUser(UserLoginDto loginDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Query()
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email, cancellationToken);

        if (user == null || !user.IsActive)
        {
            throw new NotFoundException("Userio no encontrado or inactivo.");
        }

        bool isPasswordValid = ValidatePassword(loginDto.Password, user.Password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Credenciales invalidas");
        }

        await UpdateLastLogin(user);

        var jwtToken = GenerateJwtToken(user);

        return jwtToken;
    }

    //public async ActivateUser()

    private bool ValidatePassword(string inputPassword, string storedPassword)
    {
        return inputPassword == storedPassword;
    }

    private async Task<bool> EmailExists(string email)
    {
        var user = await _userRepository.Query().AnyAsync(x => x.Email == email);
        return user;
    }

    private async Task UpdateLastLogin(UserEntity user)
    {
        user.LastLogin = DateTimeOffset.UtcNow;
        await _userRepository.UpdateAsync(user);
    }

    private string GenerateJwtToken(UserEntity user)
    {
        string jwtSecretKey = _configuration["Jwt:Key"]!;
        string issuer = _configuration["Jwt:Issuer"] ?? "UserContactApi";
        string audience = _configuration["Jwt:Audience"] ?? "UserContactAudience";

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, issuer)
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }



    public async Task<UserResponseDto> UpdateUser(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var userToUpdate = await _userRepository.GetById(id, cancellationToken);

        if (userToUpdate == null)
            throw new NotFoundException("Usuario no encontrado");

        userToUpdate.Name = updateUserDto.Name;
        userToUpdate.Email = updateUserDto.Email;
        userToUpdate.Password = updateUserDto.Password;

        await _userRepository.UpdateAsync(userToUpdate, cancellationToken);

        var userResponseDto = await _userRepository.Query()
            .Where(u => u.Id == userToUpdate.Id)
            .Select(UserProjections.UserToUserResponseDto)
            .FirstOrDefaultAsync(cancellationToken);

        return userResponseDto;
    }


    public async Task DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.Delete(id, cancellationToken);
    }
}
