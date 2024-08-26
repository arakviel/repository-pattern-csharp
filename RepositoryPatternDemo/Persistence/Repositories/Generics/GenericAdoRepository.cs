using RepositoryPatternDemo.Persistence.Entities;
using RepositoryPatternDemo.Persistence.Repositories.Mappers;
using System.Data.Common;

namespace RepositoryPatternDemo.Persistence.Repositories.Generics;

internal abstract class GenericAdoRepository<T> : IRepository<T> where T : IEntity
{
    public ConnectionManager ConnectionManager { get; init; }
    public string TableName { get; init; }
    public IMapper<T> Mapper { get; init; }
    public List<string> Attributes { get; init; }

    protected GenericAdoRepository(ConnectionManager connectionManager, string tableName, IMapper<T> mapper, List<string> attributes)
    {
        ConnectionManager = connectionManager;
        TableName = tableName;
        Mapper = mapper;
        Attributes = attributes;
    }

    public T? Get(Guid id)
    {
        string query = $"SELECT * FROM {TableName}  WHERE Id = @Id;";

        using var connection = ConnectionManager.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = query;
        var idParameter = command.CreateParameter();
        idParameter.ParameterName = "@Id";
        idParameter.Value = id;
        command.Parameters.Add(idParameter);

        var reader = command.ExecuteReader();
        return reader.Read() ? Mapper.Map(reader) : default;
    }

    public IEnumerable<T> GetAll()
    {
        string query = $"SELECT * FROM {TableName};";

        using var connection = ConnectionManager.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = query;

        var items = new List<T>();
        var reader = command.ExecuteReader();
        while (reader.Read())
            items.Add(Mapper.Map(reader));

        return items;
    }

    public T? Find(Predicate<T> predicate)
    {
        throw new NotImplementedException();
    }

    public void Remove(Guid id)
    {
        string query = $"DELETE FROM {TableName} WHERE Id = @Id;";

        using var connection = ConnectionManager.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = query;
        var idParameter = command.CreateParameter();
        idParameter.ParameterName = "@Id";
        idParameter.Value = id;
        command.Parameters.Add(idParameter);

        command.ExecuteNonQuery();
    }

    public void Remove(T entity)
    {
        Remove((Guid)entity.Id);
    }

    public void Add(T entity)
    {
        using var connection = ConnectionManager.GetConnection();
        connection.Open();

        if (entity.Id == null)
            Insert(entity, connection);
        else
            Update(entity, connection);
    }

    private void Update(T entity, DbConnection connection)
    {
        var updateCommand = connection.CreateCommand();
        string setClause = string.Join(", ", Attributes.Select(attr => $"{attr} = @{attr}"));

        updateCommand.CommandText = $"UPDATE {TableName} SET {setClause} WHERE Id = @Id;";

        InitCommandParameters(updateCommand, entity);

        var idParameter = updateCommand.CreateParameter();
        idParameter.ParameterName = "@Id";
        idParameter.Value = entity.Id;
        updateCommand.Parameters.Add(idParameter);

        updateCommand.ExecuteNonQuery();
    }

    private void Insert(T entity, DbConnection connection)
    {
        var insertCommand = connection.CreateCommand();
        string attributes = string.Join(", ", Attributes);
        string parameters = "@" + string.Join(", @", Attributes);

        Guid id = Guid.NewGuid();
        insertCommand.CommandText = $"INSERT INTO {TableName} (Id, {attributes}) VALUES (@Id, {parameters});";

        var idParameter = insertCommand.CreateParameter();
        idParameter.ParameterName = "@Id";
        idParameter.Value = id;
        insertCommand.Parameters.Add(idParameter);

        InitCommandParameters(insertCommand, entity);

        insertCommand.ExecuteNonQuery();
        entity.Id = id;
    }

    protected abstract void InitCommandParameters(DbCommand insertCommand, T entity);
}
