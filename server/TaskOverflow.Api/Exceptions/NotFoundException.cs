namespace TaskOverflow.Api.Exceptions;

public class NotFoundException(string message = "Not found") : Exception(message)
{
    
}