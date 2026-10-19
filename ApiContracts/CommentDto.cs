namespace ApiContracts;

public class CommentDto
{
    public int CommentID { get; set; }
    public string Body { get; set; } = "";
    public int UserID { get; set; }
    public int PostID { get; set; }
}
