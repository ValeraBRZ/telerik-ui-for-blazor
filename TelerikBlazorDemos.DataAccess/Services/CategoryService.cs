using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class CategoryService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        public CategoryService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            using var dbContext = _contextFactory.CreateDbContext();
            // downloading the data before selecting fix a problem with closed db connection in sqlite.
            // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
            return dbContext.Categories.ToList().Select(CategoryDto.CategoryFunc);
        }

        public IEnumerable<CategoryDto> GetProductCategories()
        {
            using var dbContext = _contextFactory.CreateDbContext();
            // downloading the data before selecting fix a problem with closed db connection in sqlite.
            // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
            return dbContext.Categories.Include(category => category.Products).ToList().Select(CategoryDto.CategoryFunc);
        }
    }
}
