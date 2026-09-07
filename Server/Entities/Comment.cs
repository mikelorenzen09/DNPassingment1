namespace Entities;

public class Comment
{
    public string Body { get; set; }
    public int UserID { get; set; }
    public int PostID { get; set; }
    public int CommentID { get; set; }
}