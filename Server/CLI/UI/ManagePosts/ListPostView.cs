using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostView
{
    private readonly IPostRepository postRepository;
    public ListPostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public Task ShowAsync()
    {
        var posts = postRepository.GetMany();

        foreach (var post in posts)
        {
            Console.WriteLine($"[{post.Title}, {post.PostID}]");
        }
        return Task.CompletedTask;
    }
}