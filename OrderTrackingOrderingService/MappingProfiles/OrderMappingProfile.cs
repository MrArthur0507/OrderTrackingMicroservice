using AutoMapper;
using MassTransitContracts.ItemCatalogContracts;
using OrderTrackingOrdereringService.Models.DbModels;

namespace OrderTrackingOrderingService.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<ItemCreated, Item>();
        }
    }
}
