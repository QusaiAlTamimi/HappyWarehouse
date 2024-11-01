﻿using HappyWarehouseCore.Dtos;
using HappyWarehouseCore.Models;
using HappyWarehouseData.DataAccess;
using HappyWarehouseService.IServices;
using Microsoft.EntityFrameworkCore;

namespace HappyWarehouseService.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly DataContext _context;
        private readonly ILogsService _logsService;

        public WarehouseService(DataContext context,ILogsService logsService)
        {
            _context = context;
            _logsService = logsService;
        }

        public async Task<List<Warehouse>> GetAllWarehousesAsync()
        {
            return await _context.Warehouses.Include(x => x.Items).ToListAsync();
        }
        public async Task<PaginationDto<Warehouse>> GetAllWarehousesAsync(int pageNumber = 1, int pageSize = 10)
        {
            var totalRecords = await _context.Warehouses.CountAsync();

            var warehouses = await _context.Warehouses
                .Include(x => x.Items)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginationDto = new PaginationDto<Warehouse>(
                pageNumber,
                pageSize,
                totalRecords,
                warehouses
            );

            return paginationDto;
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
            await _logsService.LogCreateAsync(new LogDto { TableName = "Warehouses", RecordId = warehouse.Id.ToString() });
            return warehouse;
        }

        public async Task<Warehouse?> UpdateWarehouseAsync(Warehouse warehouse)
        {
            if (warehouse == null || warehouse.Id == 0) return null;

            _context.Entry(warehouse).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                await _logsService.LogUpdateAsync(new LogDto { TableName = "Warehouses", RecordId = warehouse.Id.ToString() });
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
            await _logsService.LogDeleteAsync(new LogDto { TableName = "Warehouses", RecordId = id.ToString() });
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
