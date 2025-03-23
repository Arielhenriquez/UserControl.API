namespace UserControl.Model.Entities;

public class UserEntity : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public DateTimeOffset LastLogin { get; set; }
    public bool IsActive { get; set; }
    public ICollection<PhoneEntity> Phones { get; set; } = [];
}
