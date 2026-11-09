using System.ComponentModel.DataAnnotations;

namespace MyFirstAPI.DTOs
{
    public class OrderItemDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}