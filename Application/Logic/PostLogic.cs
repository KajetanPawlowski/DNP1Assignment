using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Exception = System.Exception;

namespace Application.Logic;

public class PostLogic: IPostLogic
{
    private readonly IPostDAO postDao;
    private readonly IUserDAO userDao;

    public PostLogic(IPostDAO postdao, IUserDAO userdao)
    {
        this.postDao = postdao;
        this.userDao = userdao;
    }
    
    public async Task<Post> CreateAsync(PostCreationDTO dto)
    {
        ValidatePostCreationData(dto);
        
        User? user = await userDao.GetByUsernameAsync(dto.Username);
       
        if (user == null)
        {
            throw new Exception("UserNotFound");
        }
        
        Post toCreate = new()
        {
            UserId = user.UserId,
            Title = dto.Title,
            Body = dto.Body
        };
        

        Post created = await postDao.CreateAsync(toCreate);
        return created;
    }

    private void ValidatePostCreationData(PostCreationDTO dto)
    {
        ValidatePostTitle(dto.Title);
        ValidatePostBody(dto.Body);
    }

    private static void ValidatePostTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new Exception("Post has to have a title");
        }
        if (title.Length > 100)
        {
            throw new Exception("Post title can be maximum 100 characters");
        }
    }

    private static void ValidatePostBody(string body)
    {
        if(body.Length > 5000)
        {
            throw new Exception("Post can be maximum 5000 characters");
        }
    }
    
    public async Task<List<Post?>> GetAsync(SearchPostParameterDTO searchParameters)
    {
        if (searchParameters.UserId != null)
        {
            return await postDao.GetByUserIdAsync((int)searchParameters.UserId);
        }
        if (searchParameters.Username != null)
        {
            int userId = userDao.GetByUsernameAsync(searchParameters.Username).Id;
            return await postDao.GetByUserIdAsync(userId);
        }
        if (searchParameters.TitleContent != null)
        {
            return await postDao.GetByTitleAsync(searchParameters.TitleContent);
        }

        return await postDao.GetPostsAsync();
    }

    // public async Task UpdateAsync(PostUpdateDTO dto)
    // {
    //     Post? existing = await postDao.GetByIdAsync(dto.PostId);
    //     if (existing == null)
    //     {
    //         throw new Exception($"Post with ID {dto.PostId} not found!");
    //     }
    //     Post updatedPost = existing;
    //     //if null - keep the old title
    //     if (dto.NewTitle != null)
    //     {
    //         ValidatePostTitle(dto.NewTitle);
    //         updatedPost.Title = dto.NewTitle;
    //     }
    //     //if null - keep ald body
    //     if (dto.NewBody != null)
    //     {
    //         ValidatePostBody(dto.NewBody);
    //         updatedPost.Body = dto.NewBody;
    //     }
    //     updatedPost.Timestamp = DateTime.Now;
    //     
    //     await postDao.UpdateAsync(dto.PostId, updatedPost);
    // }

    public async Task<PostBasicDTO> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return new PostBasicDTO(post.Title, post.UserId, post.PostId, post.Body, post.Timestamp);
    }

    public async Task DeleteAsync(int id)
    {
        Post? todo = await postDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }
        
        await postDao.DeleteAsync(id);
    }
}