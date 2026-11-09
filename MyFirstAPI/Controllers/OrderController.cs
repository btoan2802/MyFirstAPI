using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Data;
using MyFirstAPI.DTOs;
using MyFirstAPI.Models;

namespace MyFirstApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        // 1. Lấy tất cả sản phẩm trong đơn bằng MỘT câu query
        var productIds = dto.Items.Select(i => i.ProductId).ToList();
        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        var order = new Order
        {
            CustomerName = dto.CustomerName,
            Phone = dto.Phone,
            Address = dto.Address
        };

        // 2. Kiểm tra từng món, trừ kho, lấy giá từ DATABASE
        foreach (var item in dto.Items)
        {
            if (!products.TryGetValue(item.ProductId, out var product))
            {
                return BadRequest($"Sản phẩm id {item.ProductId} không tồn tại");
            }

            if (product.Stock < item.Quantity)
            {
                return BadRequest($"Sản phẩm '{product.Name}' chỉ còn {product.Stock}");
            }

            product.Stock -= item.Quantity;

            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });
        }

        // 3. Tính tổng và lưu
        order.TotalPrice = order.Items.Sum(i => i.UnitPrice * i.Quantity);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(new { orderId = order.Id, totalPrice = order.TotalPrice });
    }
}