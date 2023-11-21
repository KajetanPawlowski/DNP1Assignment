using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DAOInterfaces;
using Domain.Model;

namespace FileData.DAOs;

public class UserFileDao : IUserDAO
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    } 
    public Task<User> CreateAsync(User user)
    {
        int userId = 0;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.UserId);
            userId++;
        }
        
        user.UserId = userId;
        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public async Task DeleteAsync(int userId)
    {
        User existing = await GetByIdAsync(userId);
        context.Users.Remove(existing);
        context.SaveChanges();
    }

    public async Task<User> AssignRoleAsync(int userId, string newRole)
    {
        User existing = await GetByIdAsync(userId);
        ValidateRole(newRole);
        existing.Role = newRole;
        
        context.SaveChanges();
        return await Task.FromResult(existing);

    }
    private void ValidateRole(string role)
    {
        switch (role)
        {
            case "admin":
            case "user":
                // Valid roles, no exception thrown
                break;

            default:
                throw new Exception("Invalid role");
        }
    }
    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<List<User>> GetAsync()
    {
        return Task.FromResult(context.Users.ToList());
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserId == userId);
        if(existing == null)
        {
            throw new Exception("User not found");
        }

        return await Task.FromResult(existing);
    }

    public Task UpdatePostCounter(int userId)
    {
        throw new NotImplementedException();
    }
}