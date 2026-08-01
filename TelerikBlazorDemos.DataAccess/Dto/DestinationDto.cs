using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class DestinationDto
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(50)]
        public string ImageUrl { get; set; }

        public static Func<Destination, DestinationDto> DestinationFunc = (destination) =>
            new DestinationDto
            {
                Id = destination.Id,
                Title = destination.Title,
                Description = destination.Description,
                ImageUrl = destination.ImageUrl
            };
    }
}
