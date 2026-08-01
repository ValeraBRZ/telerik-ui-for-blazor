using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class WeatherService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        public WeatherService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<WeatherDto> GetWeathers()
        {
            using var dbContext = _contextFactory.CreateDbContext();
            // downloading the data before selecting fix a problem with closed db connection in sqlite.
            // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
            return dbContext.Weathers.ToList().Select(WeatherDto.WeatherFunc);
        }
    }
}
