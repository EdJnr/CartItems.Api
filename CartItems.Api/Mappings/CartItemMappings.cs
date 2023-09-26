using AutoMapper;
using CartItems.Api.Dtos.CartItems;
using CartItems.Api.Models;

namespace CartItems.Api.Mappings
{
    public class CartItemMappings : Profile
    {
        public CartItemMappings()
        {
            CreateMap<CreateCartItemDto, CartItemModel>().ReverseMap();

            CreateMap<GetCartItemDto, CartItemModel>().ReverseMap();
        }
    }
}
