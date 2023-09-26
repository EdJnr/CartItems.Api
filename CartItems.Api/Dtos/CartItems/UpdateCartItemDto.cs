using System.ComponentModel.DataAnnotations;

namespace CartItems.Api.Dtos.CartItems
{
    public class UpdateCartItemDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cart Id must be greater than 0")]
        public int CartId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
