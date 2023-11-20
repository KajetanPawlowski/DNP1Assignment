using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Application.DAOInterfaces;

public interface IPostDAO
{
    Task<Post> CreateAsync(Post post);
    Task<List<Post?>> GetByUserIdAsync(int userId);
    Task<List<Post?>> GetByTitleAsync(string titleContent);
    Task<List<Post?>> GetPostsAsync();
    // Task UpdateAsync(int postId, Post updatedPost);
    Task<Post?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}