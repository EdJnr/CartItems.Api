
using AutoMapper;
using CartItems.Api.Dtos.Auth;
using CartItems.Api.Helpers;
using CartItems.Api.Interfaces.IPersistence;
using CartItems.Api.Interfaces.IServices;
using CartItems.Api.Models;
using CartItems.Api.Responses;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CartItems.Api.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        const string ItemName = "Account";

        public AccountsService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> RegisterUserAsync(RegisterUserDto requestBody)
        {
            var existingUser = (await _unitOfWork.Users.QueryAsync(
                filter: user => user.UserName == requestBody.Username)
            ).FirstOrDefault();

            if (existingUser != null) return new ApiResponse<string>
            (
                false,
                null,
                (int)HttpStatusCode.BadRequest,
                ResponseMessages.Exists(ItemName, "Username", requestBody.Username)
            );

            var hash = PasswordHasher.CreatePasswordHash(requestBody.Password);
            requestBody.Password = hash;

            var user = _mapper.Map<UserModel>(requestBody);

            await _unitOfWork.Users.CreateAsync(user);
            var success =  await _unitOfWork.SaveAsync();

            return new ApiResponse<string>
            (
                success,
                success ? ResponseMessages.Created(ItemName) : null,
                success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
                success ? ResponseMessages.OperationFailed(ItemName) : null
            );
        }

        public async Task<ApiResponse<LoginResponse>> LoginUserAsync(string username, string password)
        {
            var user = (await _unitOfWork.Users.QueryAsync
            (
                filter: user => user.UserName == username
            )).FirstOrDefault();

            //user not found
            if (user == null) return new ApiResponse<LoginResponse>
            (
                false,
                null,
                (int)HttpStatusCode.BadRequest,
                "Invalid Username or Password"
            );

            bool passwordHasMatched = PasswordHashVerifier.VerifyPassword(password, user.Password);

            if (passwordHasMatched)
            {
                string token = JwtTokenGenerator.GenerateJwtToken(
                _configuration.GetSection("JwtSettings:SecretKey").Value,
                user,
                1
                );

                var userData = _mapper.Map<UserDataDto>(user);
                var LoginResponse = new LoginResponse
                {
                    Token = token,
                    User = userData
                };

                return new ApiResponse<LoginResponse>
                (
                    true,
                    LoginResponse,
                    200,
                    null
                );
            }

            return new ApiResponse<LoginResponse>
            (
                false,
                null,
                400,
                "Invalid Username or Password"
            );
        }
    }

}
