using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAO;

public class PostEfcDao : IPostDAO
{
    private readonly GossipsDbContext context;
    
    public PostEfcDao(GossipsDbContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        post.Timestamp = DateTime.Now;
        EntityEntry<Post> added = context.Posts.Add(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public Task<List<Post?>> GetByUserIdAsync(int userId)
    {
        IQueryable<Post> query = context.Posts.AsQueryable();
        query = query.Where(post =>
            post.UserId == userId);
        List<Post> result = query.ToList();
        
        return Task.FromResult(result);
    }

    public Task<List<Post?>> GetByTitleAsync(string titleContent)
    {
        IQueryable<Post> query = context.Posts.AsQueryable();
        if (!string.IsNullOrEmpty(titleContent))
        {
            query = query.Where(post =>
                post.Title.ToLower().Contains(titleContent));
        }
        List<Post> result = query.ToList();
        
        return Task.FromResult(result);
    }

    public Task<List<Post?>> GetPostsAsync()
    {
        return Task.FromResult(context.Posts.ToList());
    }
    

    public Task<Post?> GetByIdAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(t => t.PostId == id);
        return Task.FromResult(existing);
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.PostId == id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {id} does not exist!");
        }

        context.Posts.Remove(existing); 
        await context.SaveChangesAsync();
    }
}