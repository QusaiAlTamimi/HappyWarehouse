using HappyWarehouseCore.Dtos;
using HappyWarehouseCore.Models;
using HappyWarehouseData.DataAccess;
using HappyWarehouseService.IServices;
using Microsoft.EntityFrameworkCore;

namespace HappyWarehouseService.Services
{
    public class ItemService : IItemService
    {
        private readonly DataContext _context;
        private readonly ILogsService _logsService;

        public ItemService(DataContext context, ILogsService logsService)
        {
            _context = context;
            _logsService = logsService;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }
        public async Task<PaginationDto<Item>> GetAllItemsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var totalRecords = await _context.Items.CountAsync();

            var Items = await _context.Items
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginationDto = new PaginationDto<Item>(
                pageNumber,
                pageSize,
                totalRecords,
                Items
            );

            return paginationDto;
        }
        public async Task<Item?> GetItemByIdAsync(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Item?> CreateItemAsync(Item item)
        {
            if (item == null) return null;

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            await _logsService.LogCreateAsync(new LogDto { TableName = "Items", RecordId = item.Id.ToString() });
            return item;
        }

        public async Task<Item?> UpdateItemAsync(int id, Item item)
        {
            if (id != item.Id) return null;

            _context.Entry(item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                await _logsService.LogUpdateAsync(new LogDto { TableName = "Items", RecordId = item.Id.ToString() });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ItemExistsAsync(id)) return null;
                throw;
            }
            return item;
        }

        public async Task<Item?> DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return null;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            await _logsService.LogDeleteAsync(new LogDto { TableName = "Items", RecordId = item.Id.ToString() });
            return item;
        }

        public async Task<bool> ItemExistsAsync(int id)
        {
            return await _context.Items.AnyAsync(e => e.Id == id);
        }
    }
}
