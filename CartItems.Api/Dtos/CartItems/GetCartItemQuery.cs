using System;
using System.ComponentModel.DataAnnotations;

namespace CartItems.Api.Dtos.CartItems
{
    public class GetCartItemQuery
    {
        [Phone]
        public string? phoneNumber { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public int? quantity { get; set; }
        
        public string? item { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
        public int? page { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page size must be greater than 0")]
        public int? PageSize { get; set; }  
    }
}
