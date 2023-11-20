using System;

namespace Domain.DTOs;

public class PostBasicDTO
{
    public int PostId { get; }
    public int? UserId { get; }
    public string? Title { get; }
    public string? Body { get; }
    
    public DateTime Timestamp { get; set; }
    public PostBasicDTO(string? title, int? userId, int postId, string? body, DateTime dateTime)
    {
        Title = title;
        UserId = userId;
        PostId = postId;
        Body = body;
        Timestamp = dateTime;
    }
}