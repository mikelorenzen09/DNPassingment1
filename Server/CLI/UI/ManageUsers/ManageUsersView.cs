using RepositoryContracts;

namespace CLI.UI.ManageUsers;
using System;
public class ManageUsersView
{
    private readonly IUserRepository userRepository;
    
    public  ManageUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("--- Manage Users ---");
        Console.WriteLine("1) Create user");
        Console.WriteLine("2) List users");
        Console.WriteLine("0) Back");
        
        string?  input = Console.ReadLine();
        switch (input)
        {
        case"1":
            CreateUserView createUserView = new CreateUserView(userRepository);
            await createUserView.ShowAsync();
            break;
        case "2":
            ListUserView listUserView = new ListUserView(userRepository);
            await listUserView.ShowAsync();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Invalid input.");
            break;
        }
    }
}