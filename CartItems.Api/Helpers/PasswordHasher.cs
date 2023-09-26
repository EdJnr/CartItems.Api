namespace CartItems.Api.Helpers
{
    public static class PasswordHasher
    {
        public static string CreatePasswordHash(string password)
        {
            //generate a new salt if salt is not provided
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }
    }
}
