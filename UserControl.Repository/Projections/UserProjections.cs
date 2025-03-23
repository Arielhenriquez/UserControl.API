using System.Linq.Expressions;
using UserControl.Core.Dtos.Phones;
using UserControl.Core.Dtos.Users;
using UserControl.Model.Entities;


namespace UserControl.Repository.Projections;

public static class UserProjections
{
    public static Expression<Func<UserEntity, UserResponseDto>> UserToUserResponseDto = user => new UserResponseDto
    {
        Name = user.Name,
        Email = user.Email,
        LastLogin = user.LastLogin,
        IsActive = user.IsActive,
        Created = user.Created,
        Modified = user.Modified,
        Phones = user.Phones.Select(phone => new PhoneDto
        {
            Number = phone.PhoneNumber,
            CityCode = phone.CityCode,
            CountryCode = phone.CountryCode
        }).ToList()
    };
}

