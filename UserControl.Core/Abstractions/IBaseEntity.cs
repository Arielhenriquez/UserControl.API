namespace UserControl.Core.Abstractions;

public interface IBaseEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
}
