namespace CLI.UI.ManagePosts;
using RepositoryContracts;
using Entities;
using System.Linq;
public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository,  IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
    }

    public async Task ShowAsync(int postID)
    {
        
        Post post = await postRepository.GetSingleAsync(postID);

        Console.WriteLine(post.Title);
        Console.WriteLine(post.Body);
        
        var comments = commentRepository.GetMany().Where(c => c.PostID == postID);
        foreach (var comment in comments)
        {
            Console.WriteLine(comment.Body);
        }
        
        Console.WriteLine("add comment? (yes/no)");
        string? answer =  Console.ReadLine();
        if (answer == "yes")
        {
            Console.WriteLine("Enter body");
            string? body = Console.ReadLine();
            Console.WriteLine("Enter userID");
            int userID = int.Parse(Console.ReadLine());
            var user = userRepository.GetMany().SingleOrDefault(u => u.UserID == userID);
            if (user is null)
            {
                Console.WriteLine("No user with that ID exists.");
                return;
            }

            Comment newComment = new Comment
            {
                PostID = postID, Body = body, UserID = userID
            };
            Comment createdComment = await commentRepository.AddAsync(newComment);
            Console.WriteLine($"Comment '{createdComment.Body}' created with ID {createdComment.CommentID}.");
        }
    }
}