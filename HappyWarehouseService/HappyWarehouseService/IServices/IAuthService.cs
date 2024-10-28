using HappyWarehouseCore.Dtos;
using HappyWarehouseCore.Models;

namespace HappyWarehouseService.IServices
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> UpdateAsync(string userId, RegisterModel model);
        Task<AuthModel> ChangePasswordAsync(string userId, ChangePasswordDto model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<string> DeleteUserAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<List<ApplicationUser>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
    }
}