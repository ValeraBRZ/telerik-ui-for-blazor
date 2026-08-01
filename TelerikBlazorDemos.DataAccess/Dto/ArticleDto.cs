using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class ArticleDto
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Subtitle { get; set; }

        public DateTime Date { get; set; }

        [StringLength(50)]
        public string ImageUrl { get; set; }

        public static Func<Article, ArticleDto> ArticleFunc = (article) =>
            new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Subtitle = article.Subtitle,
                Date = article.Date,
                ImageUrl = article.ImageUrl
            };
    }
}
