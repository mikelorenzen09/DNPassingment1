using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace WebApplication.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest request)
    {
        User? user = await userRepository.GetMany()
            .SingleOrDefaultAsync(u => u.Username == request.Username);

        if (user is null)
        {
            return Unauthorized("Invalid username or password.");
        }

        if (user.Password != request.Password)
        {
            return Unauthorized("Invalid username or password.");
        }

        UserDto dto = new()
        {
            UserID = user.UserID,
            Username = user.Username
        };

        return Ok(dto);
    }
}
