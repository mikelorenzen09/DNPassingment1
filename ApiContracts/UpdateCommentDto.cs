namespace ApiContracts;

public class UpdateCommentDto
{
    public required string Body { get; set; }
    public required int UserID { get; set; }
    public required int PostID { get; set; }
}
