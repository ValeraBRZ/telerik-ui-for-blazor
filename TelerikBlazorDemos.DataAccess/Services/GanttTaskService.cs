using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class GanttTaskService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<GanttTaskDto> Tasks { get; set; }

        public GanttTaskService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Task<IEnumerable<GanttTaskDto>> GetTasksAsync()
        {
            return GetTasksAsyncInternal();
        }

        public void CreateTask(GanttTaskDto assignment, GanttTaskDto parentGanttTask)
        {
            if (!Tasks.Any())
            {
                assignment.Id = 1;
            }
            else
            {
                assignment.Id = Tasks.Max(p => p.Id) + 1;
            }

            if (parentGanttTask != null)
            {
                assignment.ParentId = parentGanttTask.Id;
            }

            Tasks.Insert(0, assignment);

            CalculateParentPercentRecursive(assignment);
            CalculateParentRangeRecursive(assignment);
        }

        public void UpdateTask(GanttTaskDto assignment, GanttTaskDto parentAssignment = null)
        {
            var foundItem = Tasks.FirstOrDefault(i => i.Id.Equals(assignment.Id));

            if (foundItem != null)
            {
                var startOffset = assignment.Start - foundItem.Start;
                if (startOffset != TimeSpan.Zero)
                {
                    MoveChildrenRecursive(foundItem, startOffset);
                }

                foundItem.ParentId = parentAssignment?.Id;
                foundItem.Title = assignment.Title;
                foundItem.Start = assignment.Start;
                foundItem.End = assignment.End;
                foundItem.PercentComplete = assignment.PercentComplete;

                CalculateParentPercentRecursive(foundItem);
                CalculateParentRangeRecursive(foundItem);
            }
        }

        public void DeleteTask(GanttTaskDto assignment)
        {
            var item = Tasks.FirstOrDefault(i => i.Id.Equals(assignment.Id));

            RemoveChildRecursive(item);

            CalculateParentPercentRecursive(item);
            CalculateParentRangeRecursive(item);
        }

        private void RemoveChildRecursive(GanttTaskDto item)
        {
            var children = GetChildren(item).ToList();

            foreach (var child in children)
            {
                RemoveChildRecursive(child);
            }

            Tasks.Remove(item);
        }

        private void CalculateParentPercentRecursive(GanttTaskDto item)
        {
            if (item.ParentId != null)
            {
                var parent = GetParent(item);

                var children = GetChildren(parent);

                if (children.Any())
                {
                    parent.PercentComplete = children.Average(i => i.PercentComplete);

                    CalculateParentPercentRecursive(parent);
                }
            }
        }

        private void CalculateParentRangeRecursive(GanttTaskDto item)
        {
            if (item.ParentId != null)
            {
                var parent = GetParent(item);

                var children = GetChildren(parent);

                if (children.Any())
                {
                    parent.Start = children.Min(i => i.Start);
                    parent.End = children.Max(i => i.End);

                    CalculateParentRangeRecursive(parent);
                }
            }
        }

        private void MoveChildrenRecursive(GanttTaskDto item, TimeSpan offset)
        {
            var children = GetChildren(item);

            foreach (var child in children)
            {
                child.Start = child.Start.Add(offset);
                child.End = child.End.Add(offset);

                MoveChildrenRecursive(child, offset);
            }
        }

        private GanttTaskDto GetParent(GanttTaskDto item)
        {
            return Tasks.FirstOrDefault(i => i.Id.Equals(item.ParentId));
        }

        private IEnumerable<GanttTaskDto> GetChildren(GanttTaskDto item)
        {
            return Tasks.Where(i => item.Id.Equals(i.ParentId));
        }

        private async Task<IEnumerable<GanttTaskDto>> GetTasksAsyncInternal()
        {
            if (Tasks == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                Tasks = await Task.FromResult(dbContext.GanttTasks.ToList().Select(GanttTaskDto.GanttTaskFunc).ToList());
            }

            return Tasks;
        }
    }
}
