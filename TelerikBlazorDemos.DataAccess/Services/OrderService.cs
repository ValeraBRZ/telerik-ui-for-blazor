using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class OrderService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<OrderDto> _orders;

        public OrderService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            return GetOrdersInternal();
        }

        public void CreateOrder(OrderDto order)
        {
            if (!_orders.Any())
            {
                order.OrderId = 1;
            }
            else
            {
                order.OrderId = _orders.Max(p => p.OrderId) + 1;
            }

            _orders.Insert(0, order);
        }

        public void UpdateOrder(OrderDto order)
        {
            var target = _orders.FirstOrDefault(p => p.OrderId == order.OrderId);
            if (target != null)
            {
                target.CustomerId = order.CustomerId;
                target.EmployeeId = order.EmployeeId;
                target.OrderDate = order.OrderDate;
                target.RequiredDate = order.RequiredDate;
                target.ShippedDate = order.ShippedDate;
                target.ShipVia = order.ShipVia;
                target.Freight = order.Freight;
                target.ShipName = order.ShipName;
                target.ShipAddress = order.ShipAddress;
                target.ShipCity = order.ShipCity;
                target.ShipRegion = order.ShipRegion;
                target.ShipPostalCode = order.ShipPostalCode;
                target.ShipCountry = order.ShipCountry;
            }
        }

        public void DeleteOrder(OrderDto order)
        {
            var target = _orders.FirstOrDefault(p => p.OrderId == order.OrderId);
            if (target != null)
            {
                _orders.Remove(target);
            }
        }

        private IEnumerable<OrderDto> GetOrdersInternal()
        {
            if (_orders == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                _orders = dbContext.Orders.ToList().Select(OrderDto.OrderFunc).ToList();
            }

            return _orders;
        }
    }
}
