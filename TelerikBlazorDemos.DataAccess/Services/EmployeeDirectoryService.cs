using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class EmployeeDirectoryService
    {

        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<EmployeeDirectoryDto> _employeeDirectories;

        public EmployeeDirectoryService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<EmployeeDirectoryDto> GetEmployeeDirectories()
        {
            return GetEmployeeDirectoriesInternal();
        }

        public void CreateEmployeeDirectory(EmployeeDirectoryDto employeeDirectory)
        {
            if (!_employeeDirectories.Any())
            {
                employeeDirectory.EmployeeId = 1;
            }
            else
            {
                employeeDirectory.EmployeeId = _employeeDirectories.Max(p => p.EmployeeId) + 1;
            }

            _employeeDirectories.Insert(0, employeeDirectory);
        }

        public void UpdateEmployeeDirectory(EmployeeDirectoryDto employeeDirectory)
        {
            var target = _employeeDirectories.FirstOrDefault(p => p.EmployeeId == employeeDirectory.EmployeeId);
            if (target != null)
            {
                target.FirstName = employeeDirectory.FirstName;
                target.LastName = employeeDirectory.LastName;
                target.ReportsTo = employeeDirectory.ReportsTo;
                target.Address = employeeDirectory.Address;
                target.City = employeeDirectory.City;
                target.Country = employeeDirectory.Country;
                target.Phone = employeeDirectory.Phone;
                target.Extension = employeeDirectory.Extension;
                target.BirthDate = employeeDirectory.BirthDate;
                target.HireDate = employeeDirectory.HireDate;
                target.Position = employeeDirectory.Position;
            }
        }

        public void DeleteEmployeeDirectory(EmployeeDirectoryDto employeeDirectory)
        {
            var target = _employeeDirectories.FirstOrDefault(p => p.EmployeeId == employeeDirectory.EmployeeId);
            if (target != null)
            {
                _employeeDirectories.Remove(target);
            }
        }

        private IEnumerable<EmployeeDirectoryDto> GetEmployeeDirectoriesInternal()
        {
            if (_employeeDirectories == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                _employeeDirectories = dbContext.EmployeeDirectories.ToList().Select(EmployeeDirectoryDto.EmployeeDirectoryFunc).ToList();
            }

            return _employeeDirectories;
        }
    }
}
