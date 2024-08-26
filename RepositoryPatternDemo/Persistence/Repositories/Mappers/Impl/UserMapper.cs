using RepositoryPatternDemo.Persistence.Entities;
using System.Data.Common;

namespace RepositoryPatternDemo.Persistence.Repositories.Mappers.Impl;

internal class UserMapper : IMapper<User>
{
    public User Map(DbDataReader reader)
    {
        var id = reader.GetGuid(reader.GetOrdinal("Id"));
        var name = reader.GetString(reader.GetOrdinal("Name"));
        var email = reader.GetString(reader.GetOrdinal("Email"));
        var password = reader.GetString(reader.GetOrdinal("Password"));
        var avatar = reader.IsDBNull(reader.GetOrdinal("Avatar")) ? null : reader.GetString(reader.GetOrdinal("Avatar"));
        var createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
        var updatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"));

        return new User(id, name, email, password, avatar, createdAt, updatedAt);
    }
}
