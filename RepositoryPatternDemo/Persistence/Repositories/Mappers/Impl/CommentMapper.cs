using RepositoryPatternDemo.Persistence.Entities;
using System.Data.Common;

namespace RepositoryPatternDemo.Persistence.Repositories.Mappers.Impl;

internal class CommentMapper : IMapper<Comment>
{
    private readonly IMapper<User> _userMapper;
    private readonly IMapper<Post> _postMapper;

    public CommentMapper(IMapper<User> userMapper, IMapper<Post> postMapper)
    {
        _userMapper = userMapper;
        _postMapper = postMapper;
    }

    public Comment Map(DbDataReader reader)
    {
        var id = reader.GetGuid(reader.GetOrdinal("Id"));
        var user = _userMapper.Map(reader);  // Мапимо `User`
        var post = _postMapper.Map(reader);  // Мапимо `Post`
        var body = reader.GetString(reader.GetOrdinal("Body"));
        var createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
        var updatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"));

        return new Comment(id, user, post, body, createdAt, updatedAt);
    }
}

