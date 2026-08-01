using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelerikBlazorDemos.Models
{
    public class CardCommentModel
    {
        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public int Likes { get; set; }

        public bool CommentLiked => Likes > 0;

        public string LikeButtonClass => CommentLiked ? "k-button-solid-primary" : string.Empty;

        public string LikeButtonIcon => CommentLiked ? "heart" : "heart-outline";

        public int CurrentTimeOffset => (DateTime.Now - DateTime).Minutes;

        public string FormattedTimeString => CurrentTimeOffset < 1 ? "<1m ago" : $"{CurrentTimeOffset}m ago";
    }
}
