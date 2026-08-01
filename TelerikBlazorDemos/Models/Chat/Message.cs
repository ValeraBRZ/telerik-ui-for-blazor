using System;
using System.Collections.Generic;
using Telerik.Blazor.Components;

namespace TelerikBlazorDemos.Models.Chat
{
    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string AuthorId { get; set; } = string.Empty;

        public string AuthorName { get; set; } = string.Empty;

        public string AuthorImageUrl { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public string ReplyToId { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public bool IsPinned { get; set; }

        public bool IsTyping { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public List<string> SuggestedActions { get; set; }

        public IEnumerable<FileSelectFileInfo> Files { get; set; } = new List<FileSelectFileInfo>();
    }
}
