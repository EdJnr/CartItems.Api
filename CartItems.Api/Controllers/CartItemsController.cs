using CartItems.Api.Dtos.CartItems;
using CartItems.Api.Helpers;
using CartItems.Api.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CartItems.Api.Controllers
{
    [Authorize]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemsService _cartItemsService;

        public CartItemsController(ICartItemsService service)
        {
            _cartItemsService = service;
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddCartItem(CreateCartItemDto requestBody)
        {
            var response = await _cartItemsService.CreateCartItemAsync(requestBody);

            if (response.Successful)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> EditCartItem(UpdateCartItemDto requestBody)
        {
            var response = await _cartItemsService.UpdateCartItemAsync(requestBody);

            if (response.Successful)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllCartItems([FromQuery] GetCartItemQuery query)
        {
            var result = await _cartItemsService.GetAllCartItemsAsync(query);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCartItem(int id)
        {
            var response = await _cartItemsService.GetCartItemAsync(id);

            if(response.Successful)
            {
                return Ok(response);
            }

            return NotFound(response);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var response = await _cartItemsService.DeleteCartItemAsync(id);

            if (response.Successful)
            {
                return Ok(response);
            }

            return NotFound(response);
        }
    }
}
