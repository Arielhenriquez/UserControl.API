namespace UserControl.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
     : base(message)
    {

    }

    public int StatusCode { get; set; } = 404;
}
