using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class UserLogic :IUserLogic
{
    private readonly IUserDAO userDao;
    private readonly IPostDAO postDao;

    public UserLogic(IUserDAO userDao, IPostDAO postDao)
    {
        this.userDao = userDao;
        this.postDao = postDao;
    }
    public async Task<User> RegisterUserAsync(UserLoginDTO dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.Username);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateRegistrationData(dto);
        User toCreate = new User 
        {
            Username = dto.Username,
            Password = dto.Password,
            Role = "user",
            PostCount = 0
        };
    
        User created = await userDao.CreateAsync(toCreate);
    
        return created;
    }

    public async Task DeleteUserAsync(string username)
    {
        User? existingUser = await userDao.GetByUsernameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        
        ICollection<Post?> userPosts = await postDao.GetByUserIdAsync(existingUser.UserId);
        
        foreach (var userPost in userPosts)
        {
            if (userPost != null) await postDao.DeleteAsync(userPost.PostId);
        }
        
        await userDao.DeleteAsync(existingUser.UserId);
    }

    public async Task<User> ValidateUserAsync(UserLoginDTO dto)
    {
        User? existingUser = await userDao.GetByUsernameAsync(dto.Username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(dto.Password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(existingUser);
    }

    public async Task<User?> AssignRoleAsync(AssignRoleDTO dto)
    {
        User? existingUser = await userDao.GetByUsernameAsync(dto.Username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }
        
        return await userDao.AssignRoleAsync(existingUser.UserId, dto.Role);
    }

    private static void ValidateRegistrationData(UserLoginDTO userToCreate)
    {
        string userName = userToCreate.Username;
        string password = userToCreate.Password;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        if (password.Length < 3)
            throw new Exception("Password must be at least 3 characters!");

        if (password.Length > 15)
            throw new Exception("Password must be less than 16 characters!");
    }
    public Task<List<User?>> GetUsersAsync()
    {
        return userDao.GetAsync();
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        return userDao.GetByIdAsync(userId);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        return userDao.GetByUsernameAsync(username);
    }
    
}