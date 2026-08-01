using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.SvgIcons;

namespace TelerikBlazorDemos.Models
{
    public class CardItem
    {
        public string AvatarFile { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string ImageFile { get; set; }

        public int Likes { get; set; }

        public bool PostLiked { get; set; }

        public string PostLikedText => $"{Likes + (PostLiked ? 1 : 0)} likes";

        public string LikeActionClass => PostLiked ? "k-button-flat-base" : string.Empty;

        public ISvgIcon LikeActionIcon => PostLiked ? SvgIcon.Heart : SvgIcon.HeartOutline;

        public bool CommentsVisible { get; set; }

        public List<CardCommentModel> Comments { get; set; }

        public string TempComment { get; set; }
    }
}
