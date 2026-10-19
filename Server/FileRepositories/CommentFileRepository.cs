namespace FileRepositories;

using Entities;
using RepositoryContracts;
using System.Text.Json;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(c => c.CommentID) : 0;
        comment.CommentID = maxId + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment? comment = comments.SingleOrDefault(c => c.CommentID == id);
        if (comment is null)
        {
            throw new NotFoundException($"Comment with ID '{id}' not found");
        }
        return comment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }


    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment? existingComment = comments.SingleOrDefault(c => c.CommentID == comment.CommentID);
        if (existingComment is null)
            {
            throw new NotFoundException($"Comment with ID '{comment.CommentID}' not found");
            }
        comments.Remove(existingComment);
        comments.Add(comment);

        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }


    public async Task DeleteAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment? deletedComment = comments.SingleOrDefault(c => c.CommentID == id);
        if (deletedComment is null)
            {
            throw new NotFoundException($"Comment with ID '{id}' not found");
            }
        comments.Remove(deletedComment);

        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
}
