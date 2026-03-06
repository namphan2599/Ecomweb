using System.Collections.Generic;

namespace ecomweb.Data.Dto
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = new();
        public string? Status { get; set; }
    }
}
