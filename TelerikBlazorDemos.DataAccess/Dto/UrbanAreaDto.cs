using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class UrbanAreaDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string City { get; set; }

        [Required]
        [StringLength(256)]
        public string Country { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        [Required]
        [StringLength(3)]
        public string CountryIso3 { get; set; }

        public int Pop1950 { get; set; }

        public int Pop1955 { get; set; }

        public int Pop1960 { get; set; }

        public int Pop1965 { get; set; }

        public int Pop1970 { get; set; }

        public int Pop1975 { get; set; }

        public int Pop1980 { get; set; }

        public int Pop1985 { get; set; }

        public int Pop1990 { get; set; }

        public int Pop1995 { get; set; }

        public int Pop2000 { get; set; }

        public int Pop2005 { get; set; }

        public int Pop2010 { get; set; }

        public int Pop2015 { get; set; }

        public int Pop2020 { get; set; }

        public int Pop2025 { get; set; }

        public int Pop2050 { get; set; }

        public static Func<UrbanArea, UrbanAreaDto> UrbanAreaFunc = (urbanArea) =>
            new UrbanAreaDto
            {
                Id = urbanArea.Id,
                City = urbanArea.City,
                Country = urbanArea.Country,
                Latitude = urbanArea.Latitude,
                Longitude = urbanArea.Longitude,
                CountryIso3 = urbanArea.CountryIso3,
                Pop1950 = urbanArea.Pop1950,
                Pop1955 = urbanArea.Pop1955,
                Pop1960 = urbanArea.Pop1960,
                Pop1965 = urbanArea.Pop1965,
                Pop1970 = urbanArea.Pop1970,
                Pop1975 = urbanArea.Pop1975,
                Pop1980 = urbanArea.Pop1980,
                Pop1985 = urbanArea.Pop1985,
                Pop1990 = urbanArea.Pop1990,
                Pop1995 = urbanArea.Pop1995,
                Pop2000 = urbanArea.Pop2000,
                Pop2005 = urbanArea.Pop2005,
                Pop2010 = urbanArea.Pop2010,
                Pop2015 = urbanArea.Pop2015,
                Pop2020 = urbanArea.Pop2020,
                Pop2025 = urbanArea.Pop2025,
                Pop2050 = urbanArea.Pop2050,
            };
    }
}
