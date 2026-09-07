namespace CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;
public class ListUserView
{
    private readonly IUserRepository userRepository;
    public ListUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public Task ShowAsync()
    {
        var users = userRepository.GetMany();

        foreach (var user in users)
        {
            Console.WriteLine($"[{user.UserID}] {user.Username}");
        }
        return Task.CompletedTask;
    }
    
    
}