
using CartItems.Api.Dtos.Items;
using CartItems.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartItems.Api.Interfaces.IServices
{
    public interface IItemsService
    {
        Task<ApiResponse<IReadOnlyList<GetItemDto>>> GetItemsAsync(string searchText);

        Task<ApiResponse<string>> AddItemAsync(CreateItemDto payload);

        Task<ApiResponse<string>> DeleteItemAsync(int id);
    }
}
