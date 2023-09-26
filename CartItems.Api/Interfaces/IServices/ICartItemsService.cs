using CartItems.Api.Dtos.CartItems;
using CartItems.Api.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartItems.Api.Interfaces.IServices
{
    public interface ICartItemsService
    {
        Task<ApiResponse<string>> CreateCartItemAsync(CreateCartItemDto payload);

        Task<ApiResponse<string>> UpdateCartItemAsync(UpdateCartItemDto payload);

        Task<ApiResponse<IReadOnlyList<GetCartItemDto>>> GetAllCartItemsAsync (GetCartItemQuery query);

        Task<ApiResponse<string>> DeleteCartItemAsync(int id);

        Task<ApiResponse<GetCartItemDto>> GetCartItemAsync(int id);
    }
}
