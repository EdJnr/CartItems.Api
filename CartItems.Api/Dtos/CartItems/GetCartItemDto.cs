using CartItems.Api.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartItems.Api.Dtos.CartItems
{
    public class GetCartItemDto
    {
        public int CartId { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }

        public ItemModel? Item { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastUpdatedOn { get; set; }
    }
}
