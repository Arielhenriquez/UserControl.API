namespace UserControl.Core.Dtos.Users;

public class UpdateUserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
