using System.Collections.Generic;
using Domain.Model;

namespace FileData;

public class DataContainer
{
    public ICollection<User> Users { get; set; }
    
    public ICollection<Post> Posts { get; set; }
}