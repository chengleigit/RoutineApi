using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routine.Api.Const;
using Routine.Api.Extensions;

namespace Routine.Api.Data
{
    public class RoutineDbContext:DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options)
        :base(options)
        {

        }

        public  DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Company>()
                .Property(i=>i.Name).IsRequired().HasMaxLength(ConstData.HasMaxLength100);
            modelBuilder.Entity<Company>()
                .Property(i => i.Intriduction).IsRequired().HasMaxLength(ConstData.HasMaxLength500);

            modelBuilder.Entity<Employee>()
                .Property(i => i.EmployeeNo).IsRequired().HasMaxLength(ConstData.HasMaxLength10);
            modelBuilder.Entity<Employee>()
                .Property(i => i.FirstName).IsRequired().HasMaxLength(ConstData.HasMaxLength50);
            modelBuilder.Entity<Employee>()
                .Property(i => i.LastName).IsRequired().HasMaxLength(ConstData.HasMaxLength50);

            modelBuilder.Entity<Employee>()
                .HasOne(i => i.Company)
                .WithMany(i => i.Employees)
                .HasForeignKey(i => i.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            //添加种子数据
            modelBuilder.Seed();

        }
    }
}
