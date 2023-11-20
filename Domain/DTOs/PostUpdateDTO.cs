namespace Domain.DTOs;

public class PostUpdateDTO
{
    public int PostId { get; }
    public string? NewTitle { get; set; }
    public string? NewBody { get; set; }

    public PostUpdateDTO(int postId, string? newTitle, string? newBody)  
    {
        PostId = postId;
        NewTitle = newTitle;
        NewBody = newBody;

    }
}