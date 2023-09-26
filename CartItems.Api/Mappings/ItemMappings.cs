using AutoMapper;
using CartItems.Api.Dtos.Items;
using CartItems.Api.Models;

namespace CartItems.Api.Mappings
{
    public class ItemMappings : Profile
    {
        public ItemMappings()
        {
            CreateMap<CreateItemDto, ItemModel>().ReverseMap();

            CreateMap<GetItemDto, ItemModel>().ReverseMap();
        }
    }
}
