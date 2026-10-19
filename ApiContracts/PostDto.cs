namespace ApiContracts;

public class PostDto
{
    public int PostID { get; set; }
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public int UserID { get; set; }
}
