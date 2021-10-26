using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<Company>().HasData(
                new Company 
                {
                    Id=Guid.NewGuid(),
                    Name="Microsoft",
                    Intriduction="Great Company"
                },
                 new Company
                 {
                     Id = Guid.NewGuid(),
                     Name = "Goolge",
                     Intriduction = "Don't be evil"
                 },
                  new Company
                  {
                      Id = Guid.NewGuid(),
                      Name = "Alipapa",
                      Intriduction = "Fubao Company"
                  }
                );
        
        }
    }
}
