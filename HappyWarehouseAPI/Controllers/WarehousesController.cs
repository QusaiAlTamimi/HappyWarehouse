using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HappyWarehouseAPI.Data;
using HappyWarehouseAPI.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace HappyWarehouseAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly DataContext _context;

        public WarehousesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Warehouses
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Warehouses.Include(x => x.items).ToListAsync());
        }

        // GET: api/Warehouses/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null || _context.Warehouses == null)
            {
                return NotFound();
            }

            var Warehouse = await _context.Warehouses.Include(x => x.items).FirstOrDefaultAsync(m => m.Id == id);
            if (Warehouse == null)
            {
                return NotFound();
            }

            return Ok(Warehouse);
        }

        // POST: api/Warehouses
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Warehouse Warehouse)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Warehouse);
                    await _context.SaveChangesAsync();
                    return Ok(Warehouse);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Warehouses/5
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Warehouse Warehouse)
        {
            if (Warehouse.Id == 0)
            {
                return BadRequest("Warehouse ID mismatch.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(Warehouse);
                    await _context.SaveChangesAsync();
                    return Ok(Warehouse);
                }
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(Warehouse.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Warehouses/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid Warehouse ID.");
                }

                var Warehouse = await _context.Warehouses.FindAsync(id);
                if (Warehouse == null)
                {
                    return NotFound();
                }

                _context.Warehouses.Remove(Warehouse);
                await _context.SaveChangesAsync();
                return Ok(Warehouse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private bool WarehouseExists(int id)
        {
            return _context.Warehouses.Any(e => e.Id == id);
        }
    }
}
