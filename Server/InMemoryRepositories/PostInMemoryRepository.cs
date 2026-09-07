namespace InMemoryRepositories;
using Entities;
using RepositoryContracts;
public class PostInMemoryRepository : IPostRepository
{
    private readonly List<Post> posts = new();
    public PostInMemoryRepository()
    {
        posts.Add(new Post { PostID = 1, Title = "Welcome to the forum", Body = "This is the first post!", UserID = 1 });
        posts.Add(new Post { PostID = 2, Title = "Async programming tips", Body = "Always await your tasks.", UserID = 2 });
        posts.Add(new Post { PostID = 3, Title = "C# best practices", Body = "Use meaningful names.", UserID = 3 });
    }
    
    public Task<Post> AddAsync(Post post)
    {
        post.PostID = posts.Any()
            ? posts.Max(p => p.PostID) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }
    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.PostID == post.PostID);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.PostID}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.PostID == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.PostID == id);
        if (post is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        return Task.FromResult(post);
    }
    
    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }

    
}