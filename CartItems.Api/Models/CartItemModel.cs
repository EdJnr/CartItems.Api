using System.ComponentModel.DataAnnotations.Schema;

namespace CartItems.Api.Models
{
    public class CartItemModel : BaseModel
    {
        public int CartId { get; set; }

        [ForeignKey(nameof(UserModel))]
        public int UserId { get; set; }

        public UserModel User { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(ItemModel))]
        public int ItemId { get; set; }

        public ItemModel Item { get; set; }
    }
}
