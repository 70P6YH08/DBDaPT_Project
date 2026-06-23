using ShopLibrary.DTOs;
using ShopLibrary.Models;

namespace ShopLibrary.Extensions
{
    public class OrderExtension
    {
        public static OrderDto? ToDto(Order order)
        {
            if (order == null)
                return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = DateOnly.FromDateTime(order.OrderDate.Value),
                DeliveryDate = DateOnly.FromDateTime(order.DeliveryDate.Value),
                IsFinished = order.IsFinished
            };
        }

        public static IEnumerable<OrderDto?> ToDtos(IEnumerable<Order> orders)
        {
            return orders.Select(ToDto);
        }
    }
}
