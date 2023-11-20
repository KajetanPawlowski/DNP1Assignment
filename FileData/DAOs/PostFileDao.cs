using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DAOInterfaces;
using Domain.Model;


namespace FileData.DAOs;

public class PostFileDao : IPostDAO
{
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int postId = 0;
        if (context.Posts.Any())
        {
             postId = context.Posts.Max(p => p.PostId);
             postId++;
        }
        
        post.PostId = postId;
        post.Timestamp = DateTime.Now;
        
        context.Posts.Add(post);
        context.SaveChanges();
        
        return Task.FromResult(post);
    }

    public async Task<List<Post?>> GetByUserIdAsync(int userId)
    {
        IEnumerable<Post?> postsCollection = context.Posts.Where(post => post.UserId == userId);
        return await Task.FromResult(postsCollection.ToList());
    }

    public async Task<List<Post?>> GetByTitleAsync(string titleContent)
    {
        IEnumerable<Post?> postsCollection = context.Posts.Where(post => post.Title.Contains(titleContent, StringComparison.OrdinalIgnoreCase));
        return await Task.FromResult(postsCollection.ToList());
    }

    public async Task<List<Post?>> GetPostsAsync()
    {
        return await Task.FromResult(context.Posts.ToList());
    }
    public Task UpdateAsync(int postId, Post updatedPost)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.PostId == postId);
        if (existing == null)
        {
            throw new Exception("Post not found");
        }
        existing = updatedPost;
        existing.Timestamp = DateTime.Now;
        
        context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<Post?> GetByIdAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(t => t.PostId == id);
        return Task.FromResult(existing); 
    }

    public Task DeleteAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.PostId == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} does not exist!");
        }
    
        context.Posts.Remove(existing); 
        context.SaveChanges();
    
        return Task.CompletedTask;
    }
}

