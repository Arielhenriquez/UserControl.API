namespace UserControl.Model.Entities;

public class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
}
