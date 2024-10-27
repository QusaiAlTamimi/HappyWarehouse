using HappyWarehouseCore.Dtos;
using HappyWarehouseCore.Models;

namespace HappyWarehouseService.IServices
{
    public interface IWarehouseService
    {
        Task<List<Warehouse>> GetAllWarehousesAsync();
        Task<Warehouse?> GetWarehouseByIdAsync(int id);
        Task<Warehouse?> CreateWarehouseAsync(Warehouse warehouse);
        Task<Warehouse?> UpdateWarehouseAsync(Warehouse warehouse);
        Task<Warehouse?> DeleteWarehouseAsync(int id);
        Task<IEnumerable<WarehouseSelectListItem>> GetWarehouseSelectListAsync();
        Task<bool> WarehouseExistsAsync(int id);
    }
}
