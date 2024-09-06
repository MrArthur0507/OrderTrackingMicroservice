using AutoMapper;
using MassTransitContracts.ItemCatalogContracts;
using OrderTrackingItemCatalogService.Models.ItemCatalogModels;
using OrderTrackingItemCatalogService.Models.ItemCatalogModels.DTO;

namespace OrderTrackingItemCatalogService.MappingProfiles
{
    public class ItemCatalogProfile : Profile
    {
        public ItemCatalogProfile()
        {
            CreateMap<ItemCreateDto, Item>();
            CreateMap<Item, ItemCreated>();
            CreateMap<Item, ItemUpdated>();
        }
    }
}
