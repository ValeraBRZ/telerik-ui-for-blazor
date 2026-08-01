using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class SupplierService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        public SupplierService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<SupplierDto> GetSuppliers()
        {
            using var dbContext = _contextFactory.CreateDbContext();
            // downloading the data before selecting fix a problem with closed db connection in sqlite.
            // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
            return dbContext.Suppliers.ToList().Select(SupplierDto.SupplierFunc);
        }
    }
}
