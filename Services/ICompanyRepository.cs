using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Services
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompanyAsync(Guid Id);
        Task<IEnumerable<Company>> GetCompaniesAsync();
        

        Task AddCompanyAsync(Company company);
        void UpdateCompanyAsync(Company company);
        void DeleteCompanyAsync(Company company);

        Task<bool> CompanyExistsAsync(Guid companyId);

        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);

        void AddEmployeeAsync(Guid companyId, Employee employee);
        void UpdateEmployeeAsync(Employee employee);
        void DeleteEmployeeAsync(Employee employee);

        Task<bool> SaveAsync();






    }
}
