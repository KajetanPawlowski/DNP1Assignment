using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Application.DAOInterfaces;

public interface IUserDAO
{
    Task<User> CreateAsync(User user);
    Task DeleteAsync(int userId);
    Task<User?> AssignRoleAsync(int userId, string newRole);
    Task<User?> GetByUsernameAsync(string username);
    Task<List<User?>> GetAsync();
    Task<User?> GetByIdAsync(int userId);
}
