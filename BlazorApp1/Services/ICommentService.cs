using ApiContracts;

namespace BlazorApp1.Services;

public interface ICommentService
{
    Task<CommentDto> AddCommentAsync(CreateCommentDto request);
    Task UpdateCommentAsync(int id, UpdateCommentDto request);
    Task<CommentDto> GetCommentAsync(int id);
    Task<List<CommentDto>> GetCommentsForPostAsync(int postId);
    Task DeleteCommentAsync(int id);
}
