using System;
using System.ComponentModel.DataAnnotations;

namespace CartItems.Api.Dtos.Items
{
    public class CreateItemDto
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "UnitPrice must be greater or equal to 0.01.")]
        public decimal UnitPrice { get; set; }

    }
}
