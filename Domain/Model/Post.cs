
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class Post
{
    [Key]
    public int PostId { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public string Body { get; set; }
    public DateTime Timestamp { get; set; }


}