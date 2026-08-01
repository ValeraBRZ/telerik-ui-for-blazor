using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class ShipperService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        public ShipperService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<ShipperDto> GetShippers()
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            // downloading the data before selecting fix a problem with closed db connection in sqlite.
            // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
            return _dbContext.Shippers.ToList().Select(ShipperDto.ShipperFunc);
        }
    }
}
