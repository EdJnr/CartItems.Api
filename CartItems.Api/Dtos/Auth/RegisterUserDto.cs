namespace CartItems.Api.Dtos.Auth
{
    public class RegisterUserDto
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

    }
}
