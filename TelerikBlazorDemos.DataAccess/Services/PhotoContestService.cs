using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class PhotoContestService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<PhotoContestDto> _photoContests;

        public PhotoContestService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<PhotoContestDto> GetPhotoContests()
        {
            return GetPhotoContestsInternal();
        }

        public IEnumerable<PhotoContestDto> GetPhotoContestsInternal()
        {
            if (_photoContests == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                _photoContests = dbContext.PhotoContests.ToList().Select(PhotoContestDto.PhotoContestFunc).ToList();
            }

            return _photoContests;
        }
    }
}
