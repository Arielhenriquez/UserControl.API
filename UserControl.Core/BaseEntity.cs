using UserControl.Model.Entities;

namespace UserControl.Core;

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
}
