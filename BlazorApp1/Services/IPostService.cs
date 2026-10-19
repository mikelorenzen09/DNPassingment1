using ApiContracts;

namespace BlazorApp1.Services;

public interface IPostService
{
    Task<PostDto> AddPostAsync(CreatePostDto request);
    Task UpdatePostAsync(int id, UpdatePostDto request);
    Task<PostDto> GetPostAsync(int id);
    Task<List<PostDto>> GetPostsAsync(string? title = null, int? authorId = null, string? authorName = null);
    Task DeletePostAsync(int id);
}
