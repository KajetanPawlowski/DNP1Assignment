using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    // Post User
    [HttpPost, AllowAnonymous]

    public async Task<ActionResult<User>> CreateAsync(UserLoginDTO dto)
    {
        try
        {
            User user = await userLogic.RegisterUserAsync(dto);
            return Created($"/users/{user.Username}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    //GET User
    [HttpGet, Authorize(Policy = "isUser")]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username)
    {
        try
        {
            IEnumerable<User> users = await userLogic.GetUsersAsync(); 
            if (username != null)
            {
                var userByUsername = await userLogic.GetByUsernameAsync(username);
                    users = userByUsername != null ? new List<User> { userByUsername } : Enumerable.Empty<User>();
            }

            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    //PATCH User (role assignment
    [HttpPatch, Authorize(Policy = "isAdmin")]
    public async Task<ActionResult<User>> AssignRoleAsync(AssignRoleDTO dto)
    {
        try
        {
            User? user = await userLogic.AssignRoleAsync(dto);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //DELETE User
    [HttpDelete, Authorize(Policy = "isAdmin")]
    public async Task<ActionResult> DeleteUserAsync([FromQuery] string? username)
    {
        if (username == null)
        {
            return Ok();
        }
        try
        {
            await userLogic.DeleteUserAsync(username);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}