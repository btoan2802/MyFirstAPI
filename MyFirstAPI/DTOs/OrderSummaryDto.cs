namespace MyFirstAPI.DTOs
{
    public class OrderSummaryDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<OrderLineDto> Items { get; set; } = new();
    }
}