namespace ETradeAPI.Application.Abstractions.Services;

public interface IRoleService
{
    Task<bool> CreateRoleAsync(string name);

    Task<bool> DeleteRoleAsync(string id);

    Task<bool> UpdateRoleAsync(string id, string name);

    (object,int) GetAllRolesAsync(int page,int size);

    Task<(string id, string name)> GetRoleByIdAsync(string id);
}