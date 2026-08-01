using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        public int? Position { get; set; }

        public double? Rating { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }

        public static Func<Movie, MovieDto> MovieFunc = (movie) =>
            new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Position = movie.Position,
                Rating = movie.Rating,
                Summary = movie.Summary,
            };
    }
}
