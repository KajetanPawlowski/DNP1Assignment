using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.DTOs;

namespace HttpClients.Interfaces;

public interface IAuthHttpClient
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync(string username);
    public Task RegisterAsync(string username, string password);
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public string? Jwt { get; }
}