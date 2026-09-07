namespace CLI.UI;
using Entities;
using RepositoryContracts;
using CLI.UI.ManageUsers;
using CLI.UI.ManagePosts;
public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;

    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    public async Task StartAsync()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("--- Main Menu ---");
                Console.WriteLine("1) Manage Users");
                Console.WriteLine("2) Manage Posts");
                Console.WriteLine("0) Exit");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageUsersView manageUsersView = new ManageUsersView(userRepository);
                        await manageUsersView.ShowAsync();
                        break;
                    case "2":
                        ManagePostsView managePostsView = new ManagePostsView(postRepository, commentRepository, userRepository);
                        await managePostsView.ShowAsync();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break; 
                } 
            }
         }
    
}