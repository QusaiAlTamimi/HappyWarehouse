using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HappyWarehouseData.DataAccess;
using HappyWarehouseCore.Models;
using HappyWarehouseService.IServices;

namespace HappyWarehouseAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehousesController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        // GET: api/Warehouses
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();
            return Ok(warehouses);
        }

        // GET: api/Warehouses/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null) return NotFound();

            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id.Value);
            if (warehouse == null) return NotFound();

            return Ok(warehouse);
        }

        // POST: api/Warehouses
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Warehouse warehouse)
        {
            if (!ModelState.IsValid) return BadRequest();

            var createdWarehouse = await _warehouseService.CreateWarehouseAsync(warehouse);
            if (createdWarehouse == null) return BadRequest("Failed to create warehouse.");

            return Ok(createdWarehouse);
        }

        // PUT: api/Warehouses/5
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Warehouse warehouse)
        {
            if (!ModelState.IsValid || warehouse.Id == 0) return BadRequest("Invalid warehouse data or ID mismatch.");

            var updatedWarehouse = await _warehouseService.UpdateWarehouseAsync(warehouse);
            if (updatedWarehouse == null) return NotFound();

            return Ok(updatedWarehouse);
        }

        // DELETE: api/Warehouses/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid Warehouse ID.");

            var deletedWarehouse = await _warehouseService.DeleteWarehouseAsync(id);
            if (deletedWarehouse == null) return NotFound();

            return Ok(deletedWarehouse);
        }
    }
}
