namespace ETradeAPI.Application.Abstractions.Services;

public interface IRoleService
{
    Task<bool> CreateRoleAsync(string name);

    Task<bool> DeleteRoleAsync(string name);

    Task<bool> UpdateRoleAsync(string id, string name);

    Task<Dictionary<string, string>> GetAllRolesAsync();

    Task<(string id, string name)> GetRoleByIdAsync(string id);
}