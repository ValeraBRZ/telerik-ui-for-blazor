using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class WeatherDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Station { get; set; }

        public DateTime Date { get; set; }

        public decimal Tmax { get; set; }

        public decimal Tmin { get; set; }

        public decimal Wind { get; set; }

        public decimal? Gust { get; set; }

        public decimal Rain { get; set; }

        public decimal? Snow { get; set; }

        [StringLength(255)]
        public string Events { get; set; }

        public static Func<Weather, WeatherDto> WeatherFunc = (weather) =>
            new WeatherDto
            {
                Id = weather.Id,
                Station = weather.Station,
                Date = weather.Date,
                Tmax = weather.Tmax,
                Tmin = weather.Tmin,
                Wind = weather.Wind,
                Gust = weather.Gust,
                Rain = weather.Rain,
                Snow = weather.Snow,
                Events = weather.Events
            };
    }
}
