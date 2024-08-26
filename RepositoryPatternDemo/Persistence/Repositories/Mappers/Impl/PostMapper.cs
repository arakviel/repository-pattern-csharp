using RepositoryPatternDemo.Persistence.Entities;
using System.Data.Common;

namespace RepositoryPatternDemo.Persistence.Repositories.Mappers.Impl;

internal class PostMapper : IMapper<Post>
{
    private readonly IMapper<User> _userMapper;
    private readonly IMapper<Tag> _tagMapper;

    public PostMapper(IMapper<User> userMapper, IMapper<Tag> tagMapper)
    {
        _userMapper = userMapper;
        _tagMapper = tagMapper;
    }

    public Post Map(DbDataReader reader)
    {
        var id = reader.GetGuid(reader.GetOrdinal("Id"));
        var user = _userMapper.Map(reader);  // Припускаючи, що User дані присутні в тому ж reader
        var slug = reader.GetString(reader.GetOrdinal("Slug"));
        var title = reader.GetString(reader.GetOrdinal("Title"));
        var body = reader.GetString(reader.GetOrdinal("Body"));
        var image = reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image"));
        var publishedAt = reader.IsDBNull(reader.GetOrdinal("PublishedAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("PublishedAt"));
        var createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
        var updatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"));

        // Звичайно, Tags потребують окремого мапінгу, наприклад, за допомогою іншого запиту
        var tags = new List<Tag>(); // Це місце, де ви могли б додати код для отримання тегів

        return new Post(id, user, slug, title, body, image, publishedAt, createdAt, updatedAt)
        {
            Tags = tags
        };
    }
}
