namespace Ordering.Application.Exceptions;

public class NotFoundException(string name, object key) 
    : ApplicationException($"Entity with name \"{name}\" and key \"{key}\" was not found")
{
}
