using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> RegisterUserAsync(UserLoginDTO dto);
    Task DeleteUserAsync(string username);
    Task<User> ValidateUserAsync(UserLoginDTO dto);
    Task<User?> AssignRoleAsync(AssignRoleDTO dto);
    Task<List<User?>> GetUsersAsync();
    Task<User?> GetByIdAsync(int userId);
    Task<User?> GetByUsernameAsync(string username);

    
}