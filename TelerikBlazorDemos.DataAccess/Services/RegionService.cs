using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class RegionService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        public RegionService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<RegionDto> GetRegions()
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            // downloading the data before selecting fix a problem with closed db connection in sqlite.
            // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
            return _dbContext.Regions.ToList().Select(RegionDto.RegionFunc);
        }
    }
}
