using System;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class GanttResourceAssignmentDto
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int ResourceId { get; set; }

        public double Units { get; set; }

        public static Func<GanttResourceAssignment, GanttResourceAssignmentDto> GanttResourceAssignmentFunc = (ganttResourceAssignment) =>
            new GanttResourceAssignmentDto
            {
                Id = ganttResourceAssignment.Id,
                TaskId = ganttResourceAssignment.TaskId,
                ResourceId = ganttResourceAssignment.ResourceId,
                Units = ganttResourceAssignment.Units,
            };
    }
}
