namespace CLI.UI.ManagePosts;
using RepositoryContracts;



public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;

    public ManagePostsView(IPostRepository postRepository,  ICommentRepository commentRepository,  IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
        {
            Console.WriteLine("--- Manage Posts ---");
            Console.WriteLine("1) Create post");
            Console.WriteLine("2) List posts");
            Console.WriteLine("3) View Specific Post");
            Console.WriteLine("0) Back");

            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    CreatePostView createPostView = new CreatePostView(postRepository, userRepository);
                    await createPostView.ShowAsync();
                    break;
                case "2":
                    ListPostView listPostView = new ListPostView(postRepository);
                    await listPostView.ShowAsync();
                    break;
                case "3":
                    Console.WriteLine("Enter post ID");
                    
                    int postId = int.Parse(Console.ReadLine()!);
                    var existingPost = postRepository.GetMany().SingleOrDefault(p => p.PostID == postId);
                    if (existingPost is null)
                    {
                        Console.WriteLine("No post with that ID exists.");
                        return;
                    }
                    SinglePostView singlePostView = new SinglePostView(postRepository, commentRepository,  userRepository);
                    await singlePostView.ShowAsync(postId);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        }
    }