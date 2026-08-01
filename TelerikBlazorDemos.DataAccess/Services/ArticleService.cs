using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class ArticleService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        public ArticleService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<ArticleDto> GetArticles()
        {
            using var dbContext = _contextFactory.CreateDbContext();
            // downloading the data before selecting fix a problem with closed db connection in sqlite.
            // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
            return dbContext.Articles.ToList().Select(ArticleDto.ArticleFunc);
        }
    }
}
