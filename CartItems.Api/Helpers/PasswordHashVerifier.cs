namespace CartItems.Api.Helpers
{
    public static class PasswordHashVerifier
    {
        public static bool VerifyPassword(string newPassword, string hashedPassword)
        {
            bool result = BCrypt.Net.BCrypt.Verify(newPassword, hashedPassword);
            return result;
        }
    }
}
