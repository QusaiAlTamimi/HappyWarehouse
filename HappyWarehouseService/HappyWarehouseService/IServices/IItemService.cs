using HappyWarehouseCore.Dtos;
using HappyWarehouseCore.Models;

namespace HappyWarehouseService.IServices
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItemsAsync();
        Task<PaginationDto<Item>> GetAllItemsAsync(int pageNumber = 1, int pageSize = 10);
        Task<Item?> GetItemByIdAsync(int id);
        Task<Item?> CreateItemAsync(Item item);
        Task<Item?> UpdateItemAsync(int id, Item item);
        Task<Item?> DeleteItemAsync(int id);
        Task<bool> ItemExistsAsync(int id);
    }
}
