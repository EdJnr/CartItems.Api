using AutoMapper;
using CartItems.Api.Dtos.Auth;
using CartItems.Api.Models;

namespace CartItems.Api.Mappings
{
    public class AccountMappings : Profile
    {
        public AccountMappings()
        {
            CreateMap<RegisterUserDto, UserModel>();

            CreateMap<UserModel, UserDataDto>();
        }
    }
}
