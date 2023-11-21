using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Model;
using HttpClients.Interfaces;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserHttpClient
{
    private readonly HttpClient client;
    private readonly IAuthHttpClient authHttpClient;

    public UserHttpClient(HttpClient client, IAuthHttpClient authHttpClient)
    {
        this.client = client;
        this.authHttpClient = authHttpClient;
    }
    public async Task<List<User>> GetUsersAsync()
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHttpClient.Jwt);
        HttpResponseMessage response = await client.GetAsync("/User");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<User> users = JsonSerializer.Deserialize<List<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }

    public async Task ChangeRoleAsync(string username, bool isAdmin)
    {
        string newRole = isAdmin ? "user" : "admin";
        AssignRoleDTO dto = new()
            {
                Username = username,
                Role = newRole
            };
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHttpClient.Jwt);
        
        // Manually serialize the dto to JSON
        string serializedDto = JsonSerializer.Serialize(dto);

        // Create an HttpRequestMessage with the serialized JSON
        HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), "/User");
        request.Content = new StringContent(serializedDto, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.SendAsync(request);
        
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }
    }

    public async Task RemoveUser(string username)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHttpClient.Jwt);
        
        string uri = "/User?username=";
        string query = username;
        HttpResponseMessage response = await client.DeleteAsync(uri+query);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

    }
    
}