using CartItems.Api.Dtos.Auth;

namespace CartItems.Api.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public UserDataDto User { get; set; }
    }
}
