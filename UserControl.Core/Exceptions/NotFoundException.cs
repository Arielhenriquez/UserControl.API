namespace UserControl.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
     : base(message)
    {

    }
    public NotFoundException(string entityName, Guid id)
      : base($"{entityName} con id: {id} not encontrado")
    {
        
    }

    public int StatusCode { get; set; } = 404;
}
