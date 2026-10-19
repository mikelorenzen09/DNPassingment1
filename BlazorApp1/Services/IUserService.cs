using ApiContracts;

namespace BlazorApp1.Services;

public interface IUserService
{
    Task<UserDto> AddUserAsync(CreateUserDto request);
    Task UpdateUserAsync(int id, UpdateUserDto request);
    Task<UserDto> GetUserAsync(int id);
    Task<List<UserDto>> GetUsersAsync(string? username = null);
    Task DeleteUserAsync(int id);
}
