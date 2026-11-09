using System.ComponentModel.DataAnnotations;

namespace MyFirstAPI.DTOs
{
    public class CreateOrderDto
    {
        [Required, MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        [Required, MaxLength(300)]
        public string Address { get; set; } = string.Empty;

        [Required, MinLength(1)]
        public List<OrderItemDto> Items { get; set; } = new();
    }
}