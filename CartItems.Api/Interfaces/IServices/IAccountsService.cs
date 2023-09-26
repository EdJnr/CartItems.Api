using CartItems.Api.Dtos.Auth;
using CartItems.Api.Responses;
using System.Threading.Tasks;

namespace CartItems.Api.Interfaces.IServices
{
    public interface IAccountsService
    {
        Task<ApiResponse<string>> RegisterUserAsync(RegisterUserDto requestBody);

        Task<ApiResponse<LoginResponse>> LoginUserAsync(string username, string password);
    }
}
