using RepositoryPatternDemo.Persistence.Entities;
using System.Data.Common;

namespace RepositoryPatternDemo.Persistence.Repositories.Mappers;

internal interface IMapper<T> where T : IEntity
{
    T Map(DbDataReader reader);
}
