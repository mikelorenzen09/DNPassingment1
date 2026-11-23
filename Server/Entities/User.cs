namespace Entities;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int UserID { get; set; }

    // Navigation properties for EFC (Part 7) - a user can have many posts and comments.
    public List<Post> Posts { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}
