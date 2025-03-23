namespace UserControl.Model.Entities;

public class PhoneEntity : BaseEntity
{
    public required string PhoneNumber { get; set; }
    public required string CityCode { get; set; }
    public required string CountryCode { get; set; }
    public Guid UserId { get; set; }
    public UserEntity? UserEntity { get; set; }
}
