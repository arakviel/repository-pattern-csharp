using RepositoryPatternDemo.Persistence.Entities;
using System.Data.Common;

namespace RepositoryPatternDemo.Persistence.Repositories.Mappers.Impl;

internal class TagMapper : IMapper<Tag>
{
    public Tag Map(DbDataReader reader)
    {
        var id = reader.GetGuid(reader.GetOrdinal("Id"));
        var slug = reader.GetString(reader.GetOrdinal("Slug"));
        var name = reader.GetString(reader.GetOrdinal("Name"));
        var description = reader.GetString(reader.GetOrdinal("Description"));

        return new Tag(id, slug, name, description);
    }
}