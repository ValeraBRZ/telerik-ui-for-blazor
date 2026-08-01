using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class CustomerService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        public CustomerService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {
            using var dbContext = _contextFactory.CreateDbContext();
            // downloading the data before selecting fix a problem with closed db connection in sqlite.
            // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
            return dbContext.Customers.ToList().Select(CustomerDto.CustomerFunc);
        }
    }
}
