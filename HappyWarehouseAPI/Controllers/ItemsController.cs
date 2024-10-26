using HappyWarehouseCore.Models;
using Microsoft.AspNetCore.Mvc;
using HappyWarehouseService.IServices;
using Microsoft.AspNetCore.Authorization;

namespace HappyItemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/Items
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        // GET: api/Items/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null) return NotFound();

            var item = await _itemService.GetItemByIdAsync(id.Value);
            if (item == null) return NotFound();

            return Ok(item);
        }

        // POST: api/Items
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Item item)
        {
            if (!ModelState.IsValid) return BadRequest();

            var createdItem = await _itemService.CreateItemAsync(item);
            if (createdItem == null) return BadRequest("Failed to create item.");

            return Ok(createdItem);
        }

        // PUT: api/Items/5
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Item item)
        {
            if (!ModelState.IsValid || item.Id == 0) return BadRequest("Item ID mismatch or invalid data.");

            var updatedItem = await _itemService.UpdateItemAsync(item.Id, item);
            if (updatedItem == null) return NotFound();

            return Ok(updatedItem);
        }

        // DELETE: api/Items/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid Item ID.");

            var deletedItem = await _itemService.DeleteItemAsync(id);
            if (deletedItem == null) return NotFound();

            return Ok(deletedItem);
        }
    }
}
