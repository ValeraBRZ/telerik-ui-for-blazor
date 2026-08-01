using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class MovieService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<MovieDto> _movies;

        public MovieService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<MovieDto> GetMovies()
        {
            return GetMoviesInternal();
        }

        public IEnumerable<MovieDto> GetMoviesInternal()
        {
            if (_movies == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                _movies = dbContext.Movies.ToList().Select(MovieDto.MovieFunc).ToList();
            }

            return _movies;
        }
    }
}
