using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Blazor.Extensions;

namespace TelerikBlazorDemos.Models.FileManager
{
    public class HierarchicalFileEntry : FileEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public List<HierarchicalFileEntry> Items { get; set; } = new List<HierarchicalFileEntry>();

        public List<HierarchicalFileEntry> Directories { get; set; } = new List<HierarchicalFileEntry>();
    }
}
