using FluentValidation;
using Microsoft.Extensions.Configuration;
using UserControl.Core.Dtos.Users;

namespace UserControl.Services.Validators;

public class UserValidators : AbstractValidator<CreateUserDto>
{
    public UserValidators(IConfiguration configuration)
    {
        string passwordRegexPattern = configuration["PasswordConfig:PasswordRegexPattern"]!;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .Length(5, 30).WithMessage("El nombre debe tener entre 5 y 30 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("El correo electrónico no es válido.")
            .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage("El correo electrónico debe ser válido y tener un dominio como .com, .org, .do, etc.")
            .Length(12,40).WithMessage("El correo electrónico no debe exceder los 40 caracteres.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .Matches(passwordRegexPattern)
            .WithMessage("La contraseña debe tener al menos 7 caracteres, incluir al menos una letra mayúscula, una letra minúscula y un carácter especial.");
    }
}


public class UpdateUserValidators : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidators(IConfiguration configuration)
    {
        string passwordRegexPattern = configuration["PasswordConfig:PasswordRegexPattern"]!;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .Length(5, 30).WithMessage("El nombre debe tener entre 5 y 30 caracteres.");

        RuleFor(x => x.Email)
                  .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                  .EmailAddress().WithMessage("El correo electrónico no es válido.")
                  .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage("El correo electrónico debe ser válido y tener un dominio como .com, .org, .do, etc.")
                  .Length(12, 40).WithMessage("El correo electrónico no debe exceder los 40 caracteres.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
             .Matches(passwordRegexPattern)
            .WithMessage("La contraseña debe tener al menos 7 caracteres, incluir al menos una letra mayúscula, una letra minúscula y un carácter especial.");
    }
}
