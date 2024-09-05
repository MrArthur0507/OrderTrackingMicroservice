using OrderTrackingItemCatalogService.Models.ItemCatalogModels;
using OrderTrackingItemCatalogService.Models.ItemCatalogModels.DTO;

namespace OrderTrackingItemCatalogService.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItems();
        Task<Item> GetItemById(Guid id);
        Task<ItemCreateDto> CreateItem(ItemCreateDto item);
        Task<Item> UpdateItem(Guid id, Item item);
        Task<bool> DeleteItem(Guid id);
    }
}
