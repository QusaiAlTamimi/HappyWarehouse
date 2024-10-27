using HappyWarehouseCore.Dtos;
using HappyWarehouseCore.Models;
using HappyWarehouseData.DataAccess;
using HappyWarehouseService.IServices;
using Microsoft.EntityFrameworkCore;

namespace HappyWarehouseService.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly DataContext _context;

        public WarehouseService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Warehouse>> GetAllWarehousesAsync()
        {
            return await _context.Warehouses.Include(x => x.Items).ToListAsync();
        }

        public async Task<Warehouse?> GetWarehouseByIdAsync(int id)
        {
            return await _context.Warehouses.Include(x => x.Items)
                                             .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Warehouse?> CreateWarehouseAsync(Warehouse warehouse)
        {
            if (warehouse == null) return null;

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<Warehouse?> UpdateWarehouseAsync(Warehouse warehouse)
        {
            if (warehouse == null || warehouse.Id == 0) return null;

            _context.Entry(warehouse).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WarehouseExistsAsync(warehouse.Id)) return null;
                throw;
            }
            return warehouse;
        }

        public async Task<Warehouse?> DeleteWarehouseAsync(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null) return null;

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<bool> WarehouseExistsAsync(int id)
        {
            return await _context.Warehouses.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<WarehouseSelectListItem>> GetWarehouseSelectListAsync()
        {
            return await _context.Warehouses
                .Select(w => new WarehouseSelectListItem
                {
                    Id = w.Id,
                    Name = w.Name
                }).ToListAsync();
        }
    }
}
