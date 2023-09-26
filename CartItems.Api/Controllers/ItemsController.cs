using CartItems.Api.Dtos.Items;
using CartItems.Api.Interfaces.IServices;
using CartItems.Api.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CartItems.Api.Controllers
{
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService service)
        {
            _itemsService = service;
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddItem(CreateItemDto requestBody)
        {
            var response = await _itemsService.AddItemAsync(requestBody);

            if (response.Successful)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllItems(string searchItem)
        {
            var result = await _itemsService.GetItemsAsync(searchItem);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var response = await _itemsService.DeleteItemAsync(id);

            if (response.Successful)
            {
                return Ok(response);
            }

            return NotFound(response);
        }
    }
}
