using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class GanttDependencyService
    {

        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<GanttDependencyDto> Dependencies { get; set; }

        public GanttDependencyService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Task<IEnumerable<GanttDependencyDto>> GetDependenciesAsync()
        {
            return GetDependenciesAsyncInternal();
        }

        public void CreateDependency(int predecessorId, int successorId, int type)
        {
            var id = 1;
            if (Dependencies.Any())
            {
                id = Dependencies.Max(p => p.Id) + 1;
            }

            var dependency = new GanttDependencyDto()
            {
                Id = id,
                PredecessorId = predecessorId,
                SuccessorId = successorId,
                Type = type
            };

            Dependencies.Add(dependency);
        }

        public void UpdateDependency(GanttDependencyDto dependency, int predecessorId, int successorId, int type)
        {
            var foundDependency = Dependencies.FirstOrDefault(x => x.Id == dependency.Id);

            if (foundDependency != null)
            {
                foundDependency.PredecessorId = predecessorId;
                foundDependency.SuccessorId = successorId;
                foundDependency.Type = type;
            }
        }

        public void DeleteDependency(int id)
        {
            var item = Dependencies.FirstOrDefault(x => x.Id == id);
            Dependencies.Remove(item);
        }

        private async Task<IEnumerable<GanttDependencyDto>> GetDependenciesAsyncInternal()
        {
            if (Dependencies == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                Dependencies = await Task.FromResult(dbContext.GanttDependencies.ToList().Select(GanttDependencyDto.GanttDependencyFunc).ToList());
            }

            return Dependencies;
        }
    }
}
