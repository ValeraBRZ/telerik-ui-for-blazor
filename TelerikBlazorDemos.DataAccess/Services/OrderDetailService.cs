using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class OrderDetailService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<OrderDetailDto> _orderDetails;

        public OrderDetailService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<OrderDetailDto> GetOrderDetails()
        {
            return GetOrderDetailsInternal();
        }

        public IEnumerable<OrderDetailDto> GetOrderDetailsInternal()
        {
            if (_orderDetails == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                _orderDetails = dbContext.OrderDetails.Include("Product").Include("Order").ToList().Select(OrderDetailDto.OrderDetailFunc).ToList();
            }

            return _orderDetails;
        }
    }
}
