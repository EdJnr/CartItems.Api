using CartItems.Api.Dtos.Auth;
using CartItems.Api.Helpers;
using CartItems.Api.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CartItems.Api.Controllers
{
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService service)
        {
            _accountsService = service;
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto credentials)
        {
            var response = await _accountsService.RegisterUserAsync(credentials);

            if (response.Successful)
            {
                return Ok(response);

            }

            return BadRequest(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto credentials)
        {
            var response = await _accountsService.LoginUserAsync(credentials.Username, credentials.Password);
            return Ok(response);
        }
    }
}
