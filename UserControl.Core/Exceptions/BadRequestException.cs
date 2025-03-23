namespace UserControl.Core.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }

    public int StatusCode { get; set; } = 400;
}
