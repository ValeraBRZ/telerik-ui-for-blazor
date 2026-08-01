using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class RegionDto
    {
        public int RegionId { get; set; }

        [Required]
        [StringLength(50)]
        public string RegionDescription { get; set; }

        public static Func<Region, RegionDto> RegionFunc = (region) =>
            new RegionDto
            {
                RegionId = region.RegionId,
                RegionDescription = region.RegionDescription,
            };
    }
}
