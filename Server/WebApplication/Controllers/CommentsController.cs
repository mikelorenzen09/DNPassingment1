using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;

    public CommentsController(ICommentRepository commentRepository, IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> AddComment([FromBody] CreateCommentDto request)
    {
        Comment comment = new()
        {
            Body = request.Body,
            UserID = request.UserID,
            PostID = request.PostID
        };

        Comment created = await commentRepository.AddAsync(comment);
        CommentDto dto = ToDto(created);

        return Created($"/Comments/{dto.CommentID}", dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CommentDto>> UpdateComment(int id, [FromBody] UpdateCommentDto request)
    {
        Comment comment = new()
        {
            CommentID = id,
            Body = request.Body,
            UserID = request.UserID,
            PostID = request.PostID
        };

        await commentRepository.UpdateAsync(comment);

        return Ok(ToDto(comment));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetSingle(int id)
    {
        Comment comment = await commentRepository.GetSingleAsync(id);
        return Ok(ToDto(comment));
    }

    // GET /Comments?userId=1&userName=someText&postId=1
    [HttpGet]
    public ActionResult<IEnumerable<CommentDto>> GetMany(
        [FromQuery] int? userId,
        [FromQuery] string? userName,
        [FromQuery] int? postId)
    {
        IQueryable<Comment> comments = commentRepository.GetMany();

        if (userId.HasValue)
        {
            comments = comments.Where(c => c.UserID == userId.Value);
        }

        if (!string.IsNullOrEmpty(userName))
        {
            List<int> matchingUserIds = userRepository.GetMany()
                .Where(u => u.Username.Contains(userName))
                .Select(u => u.UserID)
                .ToList();

            comments = comments.Where(c => matchingUserIds.Contains(c.UserID));
        }

        if (postId.HasValue)
        {
            comments = comments.Where(c => c.PostID == postId.Value);
        }

        List<CommentDto> dtos = comments.Select(ToDto).ToList();

        return Ok(dtos);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment(int id)
    {
        await commentRepository.DeleteAsync(id);
        return NoContent();
    }

    private static CommentDto ToDto(Comment comment)
    {
        return new CommentDto
        {
            CommentID = comment.CommentID,
            Body = comment.Body,
            UserID = comment.UserID,
            PostID = comment.PostID
        };
    }
}
