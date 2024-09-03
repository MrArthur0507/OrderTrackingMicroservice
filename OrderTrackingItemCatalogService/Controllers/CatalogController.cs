using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderTrackingItemCatalogService.Models.ItemCatalogModels;
using OrderTrackingItemCatalogService.Services.Interfaces;

namespace OrderTrackingItemCatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IItemService _itemService;

        public CatalogController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await _itemService.GetAllItems();
            return Ok(items);
        }

        [HttpGet]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = await _itemService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(Item item)
        {
            var createdItem = await _itemService.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(Guid id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var updatedItem = await _itemService.UpdateItem(id, item);
            if (updatedItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var result = await _itemService.DeleteItem(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
