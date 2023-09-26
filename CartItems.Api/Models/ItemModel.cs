using Microsoft.EntityFrameworkCore;

namespace CartItems.Api.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

    }
}
