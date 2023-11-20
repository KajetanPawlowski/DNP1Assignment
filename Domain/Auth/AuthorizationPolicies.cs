using System.Diagnostics;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        string[] allUsersRoles = { "user", "admin" };
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("isUser", a =>
                a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, allUsersRoles));
            options.AddPolicy("isAdmin", a =>
                a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "admin"));
        });
    }
}