using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETradeAPI.Persistence.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;

    public RoleService(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<bool> CreateRoleAsync(string name)
    {


        IdentityResult result = await _roleManager.CreateAsync(new() { Name = name });
        return result.Succeeded;
    }

    public async Task<bool> DeleteRoleAsync(string name)
    {
        IdentityResult result = await _roleManager.DeleteAsync(new() { Name = name });
        return result.Succeeded;
    }

    public async Task<bool> UpdateRoleAsync(string id, string name)
    {
        IdentityResult result = await _roleManager.UpdateAsync(new() { Id = id, Name = name });
        return result.Succeeded;
    }

    public async Task<Dictionary<string, string>> GetAllRolesAsync()
    {
        return await _roleManager.Roles.ToDictionaryAsync(role => role.Id, role => role.Name);
    }

    public async Task<(string id, string name)> GetRoleByIdAsync(string id)
    {
        string role = await _roleManager.GetRoleIdAsync(new() { Id = id });

        return (id, role);
    }
}