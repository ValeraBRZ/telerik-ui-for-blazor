using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class EmployeeTerritoryDto
    {
        public int EmployeeId { get; set; }

        [StringLength(20)]
        public string TerritoryId { get; set; }

        public static Func<EmployeeTerritory, EmployeeTerritoryDto> EmployeeTerritoryFunc = (employeeTerritory) =>
            new EmployeeTerritoryDto
            {
                EmployeeId = employeeTerritory.EmployeeId,
                TerritoryId = employeeTerritory.TerritoryId,
            };
    }
}
