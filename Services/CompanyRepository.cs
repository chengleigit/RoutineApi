using Microsoft.EntityFrameworkCore;
using Routine.Api.Data;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _context;

        public CompanyRepository(RoutineDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<Company> GetCompanyAsync(Guid Id) 
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(Id));
            }
            return await _context.Companies.FirstOrDefaultAsync(i=>i.Id==Id);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds) 
        {
            if (companyIds==null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }

            return await _context.Companies
                        .Where(i => companyIds.Contains(i.Id))
                        .OrderBy(i => i.Name)
                        .ToListAsync();
        }

        public Task AddCompanyAsync(Company company)
        {
            if (company==null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            company.Id = Guid.NewGuid();

            foreach (var employee in company.Employees)
            {
                employee.Id = Guid.NewGuid();
            }

            _context.Companies.Add(company);
            return Task.CompletedTask;
        }

        public void UpdateCompanyAsync(Company company) 
        {
            _context.Entry(company).State = EntityState.Modified;
        }

        public void DeleteCompanyAsync(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            _context.Companies.Remove(company);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId) 
        {
            if (companyId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Companies.AnyAsync(i=>i.Id==companyId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId)
        {
            if (companyId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Employees
                .Where(i => i.CompanyId == companyId)
                .OrderBy(i => i.EmployeeNo)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            return await _context.Employees
                        .Where(i => i.CompanyId == companyId && i.Id == employeeId)
                        .FirstOrDefaultAsync();

        }

        public void AddEmployeeAsync(Guid companyId, Employee employee) 
        {
            if (companyId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            employee.CompanyId = companyId;
            _context.Employees.Add(employee);
        }

        public void UpdateEmployeeAsync(Employee employee) 
        {
            //
        }

        public void DeleteEmployeeAsync(Employee employee) 
        {
            _context.Employees.Remove(employee );
        }

        public async Task<bool> SaveAsync() 
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
