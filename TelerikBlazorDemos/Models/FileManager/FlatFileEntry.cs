using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Blazor.Extensions;

namespace TelerikBlazorDemos.Models.FileManager
{
    public class FlatFileEntry : FileEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string ParentId { get; set; }
    }
}
