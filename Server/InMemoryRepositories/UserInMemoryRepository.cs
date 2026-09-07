using RepositoryContracts;
using Entities;
namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> users = new();
    
    public UserInMemoryRepository()
    {
        users.Add(new User { UserID = 1, Username = "alice", Password = "password123" });
        users.Add(new User { UserID = 2, Username = "bob", Password = "hunter2" });
        users.Add(new User { UserID = 3, Username = "charlie", Password = "letmein" });
    }

    public Task<User> AddAsync(User user)
    {
        user.UserID = users.Any()
            ? users.Max(u => u.UserID) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.UserID == user.UserID);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.UserID}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(u => u.UserID == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? user = users.SingleOrDefault(u => u.UserID == id);
        if (user is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }
        return Task.FromResult(user);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
}