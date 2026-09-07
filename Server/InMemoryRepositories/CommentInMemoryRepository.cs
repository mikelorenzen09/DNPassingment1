using RepositoryContracts;
using Entities;
using RepositoryContracts;
namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private readonly List<Comment> comments = new();
    public CommentInMemoryRepository()
    {
        comments.Add(new Comment { CommentID = 1, Body = "Great post!", PostID = 1, UserID = 2 });
        comments.Add(new Comment { CommentID = 2, Body = "Thanks for sharing.", PostID = 1, UserID = 3 });
        comments.Add(new Comment { CommentID = 3, Body = "I agree with this.", PostID = 2, UserID = 1 });
    }

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.CommentID = comments.Any()
            ? comments.Max(c => c.CommentID) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.CommentID == comment.CommentID);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.CommentID}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(c => c.CommentID == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = comments.SingleOrDefault(c => c.CommentID == id);
        if (comment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }
}