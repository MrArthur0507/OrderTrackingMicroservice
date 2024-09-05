using AutoMapper;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransitContracts.ItemCatalogContracts;
using Microsoft.EntityFrameworkCore;
using OrderTrackingItemCatalogService.Data;
using OrderTrackingItemCatalogService.Models.ItemCatalogModels;
using OrderTrackingItemCatalogService.Models.ItemCatalogModels.DTO;
using OrderTrackingItemCatalogService.Services.Interfaces;

namespace OrderTrackingItemCatalogService.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly ItemCatalogContext _context;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publisher;
        public ItemService(ItemCatalogContext context, IMapper mapper, IPublishEndpoint publisher)
        {
            _context = context;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetItemById(Guid id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<ItemCreateDto> CreateItem(ItemCreateDto item)
        {
            Item itemToSave = _mapper.Map<Item>(item);
            _context.Items.Add(itemToSave);
            int changes = await _context.SaveChangesAsync();
            if (changes > 0)
            {
                await _publisher.Publish(_mapper.Map<ItemCreated>(itemToSave));
            }
            return item;
        }

        public async Task<Item> UpdateItem(Guid id, Item item)
        {
            var existingItem = await _context.Items.FindAsync(id);
            if (existingItem == null)
            {
                return null; 
            }

            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.Price = item.Price;
            existingItem.StockQuantity = item.StockQuantity;

            _context.Entry(existingItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingItem;
        }

        public async Task<bool> DeleteItem(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return false; 
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
