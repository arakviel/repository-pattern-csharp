namespace RepositoryPatternDemo.Persistence.Entities.Comparators;

internal class UserCompererByCreatedAt : IComparer<User>
{
    public int Compare(User? first, User? second) => first?.CreatedAt.CompareTo(second?.CreatedAt) ?? 0;
}
