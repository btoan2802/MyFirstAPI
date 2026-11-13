using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? UserId { get; set; }
        public User? User { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}