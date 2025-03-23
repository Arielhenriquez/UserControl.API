using UserControl.Core.Dtos.Phones;

namespace UserControl.Core.Dtos.Users;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
    public DateTimeOffset LastLogin { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public bool IsActive { get; set; }
    public required ICollection<PhoneDto> Phones { get; set; }
}
