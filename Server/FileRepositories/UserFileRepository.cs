namespace FileRepositories;

using Entities;
using RepositoryContracts;
using System.Text.Json;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<User> AddAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        int maxId = users.Count > 0 ? users.Max(u => u.UserID) : 0;
        user.UserID = maxId + 1;
        users.Add(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return user;
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        User? user = users.SingleOrDefault(u => u.UserID == id);
        if (user is null)
        {
            throw new NotFoundException($"User with ID '{id}' not found");
        }
        return user;
    }

    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users.AsQueryable();
    }

    public async Task UpdateAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        User? existingUser = users.SingleOrDefault(u => u.UserID == user.UserID);
        if (existingUser is null)
        {
            throw new NotFoundException($"User with ID '{user.UserID}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);

        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        User? deletedUser = users.SingleOrDefault(u => u.UserID == id);
        if (deletedUser is null)
        {
            throw new NotFoundException($"User with ID '{id}' not found");
        }
        users.Remove(deletedUser);

        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }
}
