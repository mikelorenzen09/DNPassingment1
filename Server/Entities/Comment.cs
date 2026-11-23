namespace Entities;

public class Comment
{
    public string Body { get; set; }
    public int UserID { get; set; }
    public int PostID { get; set; }
    public int CommentID { get; set; }

    // Navigation properties for EFC (Part 7).
    public User User { get; set; }
    public Post Post { get; set; }
}
