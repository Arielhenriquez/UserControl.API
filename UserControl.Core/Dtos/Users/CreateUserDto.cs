using UserControl.Core.Dtos.Phones;

namespace UserControl.Core.Dtos.Users;

public class CreateUserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required ICollection<PhoneDto> Phones { get; set; }
}
