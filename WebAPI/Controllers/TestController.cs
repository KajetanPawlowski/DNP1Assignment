using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TestController : ControllerBase
{
    [HttpGet("isUser")]
    [Authorize(Policy = "isUser")]
    public IActionResult UserAction()
    {
        return Ok("This is an action for users and admins.");
    }

    [HttpGet("isAdmin")]
    [Authorize(Policy = "isAdmin")]
    public IActionResult AdminAction()
    {
        return Ok("This is an action for admins.");
    }

}