using System.ComponentModel.DataAnnotations;

namespace CartItems.Api.Dtos.CartItems
{
    public class CreateCartItemDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Item Id must be greater than 0")]
        public int ItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

    }
}
