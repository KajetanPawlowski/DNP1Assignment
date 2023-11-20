using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Model;

namespace HttpClients.Interfaces;

public interface IUserHttpClient
{
    public Task<List<User>> GetUsersAsync();
    public Task ChangeRoleAsync(string username, bool isAdmin);
    public Task RemoveUser(string username);
}