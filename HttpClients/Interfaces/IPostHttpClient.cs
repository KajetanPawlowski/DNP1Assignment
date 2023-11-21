using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Model;

namespace HttpClients.Interfaces;

public interface IPostHttpClient
{
    public Task AddPost(string username, string title, string body);
    public Task<List<Post>> GetPostsAsync();
    public Task<List<Post>> GetUserPostsAsync(string username);
    
}