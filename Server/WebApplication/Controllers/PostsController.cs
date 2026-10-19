using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;

    public PostsController(IPostRepository postRepository, IUserRepository userRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> AddPost([FromBody] CreatePostDto request)
    {
        Post post = new()
        {
            Title = request.Title,
            Body = request.Body,
            UserID = request.UserID
        };

        Post created = await postRepository.AddAsync(post);
        PostDto dto = ToDto(created);

        return Created($"/Posts/{dto.PostID}", dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostDto>> UpdatePost(int id, [FromBody] UpdatePostDto request)
    {
        Post post = new()
        {
            PostID = id,
            Title = request.Title,
            Body = request.Body,
            UserID = request.UserID
        };

        await postRepository.UpdateAsync(post);

        return Ok(ToDto(post));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetSingle(int id)
    {
        Post post = await postRepository.GetSingleAsync(id);
        return Ok(ToDto(post));
    }

    // GET /Posts?title=someText&authorId=1&authorName=someText
    [HttpGet]
    public ActionResult<IEnumerable<PostDto>> GetMany(
        [FromQuery] string? title,
        [FromQuery] int? authorId,
        [FromQuery] string? authorName)
    {
        IQueryable<Post> posts = postRepository.GetMany();

        if (!string.IsNullOrEmpty(title))
        {
            posts = posts.Where(p => p.Title.Contains(title));
        }

        if (authorId.HasValue)
        {
            posts = posts.Where(p => p.UserID == authorId.Value);
        }

        if (!string.IsNullOrEmpty(authorName))
        {
            List<int> matchingUserIds = userRepository.GetMany()
                .Where(u => u.Username.Contains(authorName))
                .Select(u => u.UserID)
                .ToList();

            posts = posts.Where(p => matchingUserIds.Contains(p.UserID));
        }

        List<PostDto> dtos = posts.Select(ToDto).ToList();

        return Ok(dtos);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        await postRepository.DeleteAsync(id);
        return NoContent();
    }

    // GET /Posts/{id}/comments - nested route for the comments belonging to a post
    [HttpGet("{id}/comments")]
    public ActionResult<IEnumerable<CommentDto>> GetCommentsForPost(int id)
    {
        List<CommentDto> dtos = commentRepository.GetMany()
            .Where(c => c.PostID == id)
            .Select(c => new CommentDto
            {
                CommentID = c.CommentID,
                Body = c.Body,
                UserID = c.UserID,
                PostID = c.PostID
            })
            .ToList();

        return Ok(dtos);
    }

    private static PostDto ToDto(Post post)
    {
        return new PostDto
        {
            PostID = post.PostID,
            Title = post.Title,
            Body = post.Body,
            UserID = post.UserID
        };
    }
}
