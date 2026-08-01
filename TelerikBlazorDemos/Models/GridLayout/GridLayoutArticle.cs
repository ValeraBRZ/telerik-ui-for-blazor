using System;

namespace TelerikBlazorDemos.Models
{
    public class GridLayoutArticle
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string ImageUrl { get; set; }

        public string ImageRight { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public int MinsLength { get; set; }

        public string FormattedDate => Date.ToString("MMM dd");
    }
}
