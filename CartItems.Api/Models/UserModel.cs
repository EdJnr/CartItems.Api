namespace CartItems.Api.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

    }
}
