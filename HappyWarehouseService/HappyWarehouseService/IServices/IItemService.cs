using HappyWarehouseCore.Models;

namespace HappyWarehouseService.IServices
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task<Item?> CreateItemAsync(Item item);
        Task<Item?> UpdateItemAsync(int id, Item item);
        Task<Item?> DeleteItemAsync(int id);
        Task<bool> ItemExistsAsync(int id);
    }
}
