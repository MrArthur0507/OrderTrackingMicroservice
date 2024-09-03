using OrderTrackingItemCatalogService.Models.ItemCatalogModels;

namespace OrderTrackingItemCatalogService.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItems();
        Task<Item> GetItemById(Guid id);
        Task<Item> CreateItem(Item item);
        Task<Item> UpdateItem(Guid id, Item item);
        Task<bool> DeleteItem(Guid id);
    }
}
