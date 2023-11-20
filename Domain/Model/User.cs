using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    //admin, user, owner
    public string Role { get; set; }
}