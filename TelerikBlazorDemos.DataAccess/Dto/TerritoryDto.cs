using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class TerritoryDto
    {
        [StringLength(20)]
        public string TerritoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string TerritoryDescription { get; set; }

        public int RegionId { get; set; }

        public static Func<Territory, TerritoryDto> TerritoryFunc = (territory) =>
            new TerritoryDto
            {
                TerritoryId = territory.TerritoryId,
                TerritoryDescription = territory.TerritoryDescription,
                RegionId = territory.RegionId,
            };
    }
}
