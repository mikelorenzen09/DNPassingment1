namespace CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

public class CreateUserView
{
    private readonly IUserRepository userRepository;
    
    public  CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Enter username:");
        string username = Console.ReadLine()!;

        Console.WriteLine("Enter password:");
        string password = Console.ReadLine()!;

        User newUser = new User
        {
            Username = username,
            Password = password
        };

        var existingUser = userRepository.GetMany().SingleOrDefault(u => u.Username == username);
        if (existingUser != null)
        {
            Console.WriteLine("That username is already taken.");
            return;
        }
        
        User createdUser = await userRepository.AddAsync(newUser);
        Console.WriteLine($"User '{createdUser.Username}' created with ID {createdUser.UserID}.");
    }
}