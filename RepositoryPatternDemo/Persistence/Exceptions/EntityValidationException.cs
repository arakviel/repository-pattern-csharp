namespace RepositoryPatternDemo.Persistence.Exceptions;

internal class EntityValidationException : ArgumentException
{
    private readonly Dictionary<string, List<string>> _errors;
    public EntityValidationException(Dictionary<string, List<string>> errors) => _errors = errors;
}