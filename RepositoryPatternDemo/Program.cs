using RepositoryPatternDemo.Persistence.Entities;
using RepositoryPatternDemo.Persistence.Repositories.Impl;

namespace RepositoryPatternDemo;

internal class Program
{
    static void Main(string[] args)
    {
        // Ініціалізуємо репозиторій користувачів
        var userRepository = new UserFileRepository();

        // Створюємо 10 користувачів
        var users = new List<User>
            {
                new(Guid.NewGuid(), "Alice", "alice@example.com", "password1", null, DateTime.Now, DateTime.Now),
                new(Guid.NewGuid(), "Bob", "bob@example.com", "password2", null, DateTime.Now, DateTime.Now),
                new(Guid.NewGuid(), "Charlie", "charlie@example.com", "password3", null, DateTime.Now, DateTime.Now),
                new(Guid.NewGuid(), "David", "david@example.com", "password4", null, DateTime.Now, DateTime.Now),
                new(Guid.NewGuid(), "Eva", "eva@example.com", "password5", null, DateTime.Now, DateTime.Now),
                new(Guid.NewGuid(), "Frank", "frank@example.com", "password6", null, DateTime.Now, DateTime.Now),
                new(Guid.NewGuid(), "Grace", "grace@example.com", "password7", null, DateTime.Now, DateTime.Now),
                new(Guid.NewGuid(), "Henry", "henry@example.com", "password8", null, DateTime.Now, DateTime.Now),
                new(Guid.NewGuid(), "Ivy", "ivy@example.com", "password9", null, DateTime.Now, DateTime.Now),
                new(Guid.NewGuid(), "Jack", "jack@example.com", "password10", null, DateTime.Now, DateTime.Now)
            };

        // Додаємо користувачів до репозиторію
        foreach (var user in users)
        {
            userRepository.Add(user);
        }

        // Зберігаємо всіх користувачів у файл
        AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) =>
        {
            userRepository.SerializeAll();
            Console.WriteLine("Users have been saved to file.");
        };

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
