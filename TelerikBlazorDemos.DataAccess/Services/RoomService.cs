using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class RoomService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<RoomDto> _rooms;

        public RoomService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<RoomDto> GetRooms()
        {
            return GetRoomsInternal();
        }

        public IEnumerable<RoomDto> GetRoomsInternal()
        {
            if (_rooms == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                _rooms = dbContext.Rooms.ToList().Select(RoomDto.RoomFunc).ToList();
            }

            return _rooms;
        }
    }
}
