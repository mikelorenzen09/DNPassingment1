using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UsersController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
        User user = new()
        {
            Username = request.Username,
            Password = request.Password
        };

        User created = await userRepository.AddAsync(user);
        UserDto dto = ToDto(created);

        return Created($"/Users/{dto.UserID}", dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto request)
    {
        User user = new()
        {
            UserID = id,
            Username = request.Username,
            Password = request.Password
        };

        await userRepository.UpdateAsync(user);

        return Ok(ToDto(user));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetSingle(int id)
    {
        User user = await userRepository.GetSingleAsync(id);
        return Ok(ToDto(user));
    }

    // GET /Users?username=someText
    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetMany([FromQuery] string? username)
    {
        IQueryable<User> users = userRepository.GetMany();

        if (!string.IsNullOrEmpty(username))
        {
            users = users.Where(u => u.Username.Contains(username));
        }

        List<UserDto> dtos = users.Select(ToDto).ToList();

        return Ok(dtos);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        await userRepository.DeleteAsync(id);
        return NoContent();
    }

    private static UserDto ToDto(User user)
    {
        return new UserDto
        {
            UserID = user.UserID,
            Username = user.Username
        };
    }
}
