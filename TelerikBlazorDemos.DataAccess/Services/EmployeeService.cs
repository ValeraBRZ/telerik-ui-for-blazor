using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class EmployeeService
    {

        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<EmployeeDto> _employees;

        public EmployeeService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            return GetEmployeesInternal();
        }

        public void CreateEmployee(EmployeeDto employee)
        {
            if (!_employees.Any())
            {
                employee.EmployeeId = 1;
            }
            else
            {
                employee.EmployeeId = _employees.Max(p => p.EmployeeId) + 1;
            }

            _employees.Insert(0, employee);
        }

        public void UpdateEmployee(EmployeeDto employee)
        {
            var target = _employees.FirstOrDefault(p => p.EmployeeId == employee.EmployeeId);
            if (target != null)
            {
                target.FirstName = employee.FirstName;
                target.LastName = employee.LastName;
                target.Title = employee.Title;
                target.TitleOfCourtesy = employee.TitleOfCourtesy;
                target.BirthDate = employee.BirthDate;
                target.HireDate = employee.HireDate;
                target.Address = employee.Address;
                target.City = employee.City;
                target.Region = employee.Region;
                target.PostalCode = employee.PostalCode;
                target.Country = employee.Country;
                target.HomePhone = employee.HomePhone;
                target.Extension = employee.Extension;
                target.Notes = employee.Notes;
                target.ReportsTo = employee.ReportsTo;
            }
        }

        public void DeleteEmployee(EmployeeDto employee)
        {
            var target = _employees.FirstOrDefault(p => p.EmployeeId == employee.EmployeeId);
            if (target != null)
            {
                _employees.Remove(target);
            }
        }

        private IEnumerable<EmployeeDto> GetEmployeesInternal()
        {
            if (_employees == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                _employees = dbContext.Employees.ToList().Select(EmployeeDto.EmployeeFunc).ToList();
            }

            return _employees;
        }
    }
}
