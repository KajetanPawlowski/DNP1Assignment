using System.Security.Claims;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebApp.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly IAuthHttpClient authService;

    public CustomAuthProvider(IAuthHttpClient authService)
    {
        this.authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
            ClaimsPrincipal principal = await authService.GetAuthAsync();
            return new AuthenticationState(principal);
      
    }
    
    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}