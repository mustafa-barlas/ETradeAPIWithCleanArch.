﻿using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

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
        IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
        return result.Succeeded;
    }

    public async Task<bool> DeleteRoleAsync(string id)
    {
        AppRole role = await _roleManager.FindByIdAsync(id);
        IdentityResult result = await _roleManager.DeleteAsync(role);
        return result.Succeeded;
    }

    public async Task<bool> UpdateRoleAsync(string id, string name)
    {
        AppRole role = await _roleManager.FindByIdAsync(id);
        role.Name = name;
        IdentityResult result = await _roleManager.UpdateAsync(role);
        return result.Succeeded;
    }

    public (object, int) GetAllRolesAsync(int page, int size)
    {
        var query = _roleManager.Roles;

        IQueryable<AppRole> rolesQuery = null;

        if (page != -1 && size != -1)
            rolesQuery = query.Skip(page * size).Take(size);
        else
            rolesQuery = query;

        return (query.Select(x => new { x.Id, x.Name }), query.Count());
    }

    public async Task<(string id, string name)> GetRoleByIdAsync(string id)
    {
        string role = await _roleManager.GetRoleIdAsync(new() { Id = id });

        return (id, role);
    }
}