using System;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class GanttDependencyDto
    {
        public int Id { get; set; }

        public int PredecessorId { get; set; }

        public int SuccessorId { get; set; }

        public int Type { get; set; }

        public static Func<GanttDependency, GanttDependencyDto> GanttDependencyFunc = (ganttDependency) =>
            new GanttDependencyDto
            {
                Id = ganttDependency.Id,
                PredecessorId = ganttDependency.PredecessorId,
                SuccessorId = ganttDependency.SuccessorId,
                Type = ganttDependency.Type,
            };
    }
}
