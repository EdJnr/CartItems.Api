using CartItems.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CartItems.Api.Helpers
{
    public static class JwtTokenGenerator
    {
        public static string GenerateJwtToken(string secretKey, UserModel user, int expirationDays = 1)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("userId", user.UserId.ToString()),
                new Claim("contact", user.Contact),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : "user")
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expirationDays),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
