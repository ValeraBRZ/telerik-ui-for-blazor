using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Blazor.Extensions;

namespace TelerikBlazorDemos.Models.FileManager
{
    // escape ASP.NET-related names like FileInfo or File
    public class FileEntry
    {
        public string Name { get; set; }

        public long Size { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }

        public bool IsDirectory { get; set; }

        public bool HasDirectories { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateModifiedUtc { get; set; }

    }
}
