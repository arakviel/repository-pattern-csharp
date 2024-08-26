using RepositoryPatternDemo.Persistence.Entities;
using RepositoryPatternDemo.Persistence.Repositories.Contracts;
using RepositoryPatternDemo.Persistence.Repositories.Generics;
using RepositoryPatternDemo.Persistence.Repositories.Mappers.Impl;
using System.Data.Common;

namespace RepositoryPatternDemo.Persistence.Repositories.AdoImpl;

internal class UserAdoRepository :
    GenericAdoRepository<User>,
    IUserRepository
{
    private static readonly List<string> attributes = new List<string> { "Name", "Email", "Password", "Avatar", "CreatedAt", "UpdatedAt" };

    public UserAdoRepository(ConnectionManager connectionManager) :
        base(connectionManager, "Users", new UserMapper(), attributes)
    { }

    public User? GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    protected override void InitCommandParameters(DbCommand command, User entity)
    {
        var nameParameter = command.CreateParameter();
        nameParameter.ParameterName = $"@{attributes[0]}";
        nameParameter.Value = entity.Name;

        var emailParameter = command.CreateParameter();
        emailParameter.ParameterName = $"@{attributes[1]}";
        emailParameter.Value = entity.Email;

        var passwordParameter = command.CreateParameter();
        passwordParameter.ParameterName = $"@{attributes[2]}";
        passwordParameter.Value = entity.Password;

        var avatarParameter = command.CreateParameter();
        avatarParameter.ParameterName = $"@{attributes[3]}";
        avatarParameter.Value = entity.Avatar ?? (object)DBNull.Value;

        var createdAtParameter = command.CreateParameter();
        createdAtParameter.ParameterName = $"@{attributes[4]}";
        createdAtParameter.Value = entity.CreatedAt;

        var updatedAtParameter = command.CreateParameter();
        updatedAtParameter.ParameterName = $"@{attributes[5]}";
        updatedAtParameter.Value = entity.UpdatedAt;

        command.Parameters.AddRange(new[] { nameParameter, emailParameter, passwordParameter, avatarParameter, createdAtParameter, updatedAtParameter });
    }
}
