namespace Entities;

public class Post
{
    public int PostID { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserID { get; set; }

    // Navigation properties for EFC (Part 7).
    public User User { get; set; }
    public List<Comment> Comments { get; set; } = new();
}
