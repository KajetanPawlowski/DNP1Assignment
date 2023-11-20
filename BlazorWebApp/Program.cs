using System;
using System.Net.Http;
using BlazorWebApp.Auth;
using Domain.Auth;
using HttpClients.Implementations;
using HttpClients.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


//From Domain
AuthorizationPolicies.AddPolicies(builder.Services);

//HTTP Client
builder.Services.AddScoped(
    sp =>
        new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5038")
        }
);

builder.Services.AddScoped<IAuthHttpClient, JwtAuthClient>();
builder.Services.AddScoped<IPostHttpClient, PostHttpClient>();
builder.Services.AddScoped<IUserHttpClient, UserHttpClient>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();