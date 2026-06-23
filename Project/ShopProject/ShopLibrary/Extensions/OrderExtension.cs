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
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                IsFinished = order.IsFinished
            };
        }

        public static OrderDetailsDto? ToDtoInfo(Order order)
        {
            if (order is null)
                return null;

            return new OrderDetailsDto
            {
                OrderId = order.OrderId,
                TotalPrice = order.ShoeOrders.Sum(so => so.Shoe.Price * (1 - so.Shoe.Discount / 100) * so.Quantity),
                OrderDate = order.OrderDate,
                ShoeArticles = String.Join(", ", order.ShoeOrders.Select(so => string.Format("[{0}] {1} - {2} шт.", so.Shoe.Article, so.Shoe.Maker.Name, so.Quantity))),
                User = order.User
            };
        }


        public static IEnumerable<OrderDto?> ToDtos(IEnumerable<Order> orders)
        {
            return orders.Select(ToDto);
        }

        public static IEnumerable<OrderDetailsDto?> ToDtoInfos(IEnumerable<Order> orders)
        {
            return orders.Select(ToDtoInfo);
        }
    }
}
