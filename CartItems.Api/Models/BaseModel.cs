using System;

namespace CartItems.Api.Models
{
    public class BaseModel
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedOn { get; set; }
    }
}
