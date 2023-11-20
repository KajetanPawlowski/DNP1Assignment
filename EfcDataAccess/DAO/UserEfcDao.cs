using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAO;

public class UserEfcDao : IUserDAO
{
    private readonly GossipsDbContext context;

    public UserEfcDao(GossipsDbContext context)
    {
        this.context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task DeleteAsync(int userId)
    {
        User? user = await GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }

    public Task<User?> AssignRoleAsync(int userId, string newRole)
    {
        throw new NotImplementedException();
    }
    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Username.Equals(userName)
        );
        return existing;
    }

    public Task<List<User?>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
}