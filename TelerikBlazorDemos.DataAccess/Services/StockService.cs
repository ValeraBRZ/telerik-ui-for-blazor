using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class StockService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        public StockService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<StockDto> GetStocks()
        {
            using var dbContext = _contextFactory.CreateDbContext();
            // downloading the data before selecting fix a problem with closed db connection in sqlite.
            // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
            return dbContext.Stocks.ToList().Select(StockDto.StockFunc);
        }
    }
}
