using System;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class OrderDetailDto
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public decimal? OrderFreight { get; set; }

        public string OrderShipAddress { get; set; }

        public string OrderShipName { get; set; }

        public string OrderShipCountry { get; set; }

        public static Func<OrderDetail, OrderDetailDto> OrderDetailFunc = (orderDetail) =>
            new OrderDetailDto
            {
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                UnitPrice = orderDetail.UnitPrice,
                Quantity = orderDetail.Quantity,
                Discount = orderDetail.Discount,
                OrderFreight = orderDetail.Order.Freight,
                OrderShipAddress = orderDetail.Order.ShipAddress,
                OrderShipName = orderDetail.Order.ShipName,
                OrderShipCountry = orderDetail.Order.ShipCountry,
            };
    }
}
